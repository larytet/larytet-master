
using System;
using System.Collections.Generic;
using System.Reflection;

#if USEFMRSIM
using TaskBarLibSim;
#else
using TaskBarLib;
#endif

namespace JQuant
{
    /// <summary>
    /// This is a simulation of the Maof options market. The class collects incoming events - events taken
    /// from the historical data, keeps track of all strikes. MarketSimulation compares pending orders 
    /// with the market state and figures out if fill was possible. To make the whole exercise practical 
    /// for the current phase I assume that incoming orders do not influence the market. 
    /// I assume that if bid (ask) limit is equal or greater (less) to the best ask (bid),
    /// the probability of the fill is 1 (immediate fill).
    /// </summary>
    public class MarketSimulationMaof : MarketSimulation.Core, JQuant.IConsumer<K300MaofType>
    {
        public MarketSimulationMaof()
        {
            Type dataType = typeof(K300MaofType);
            field_BNO_Num = dataType.GetField("BNO_Num");
            field_LMT_BY1 = dataType.GetField("LMT_BY1");
            field_LMT_BY2 = dataType.GetField("LMT_BY2");
            field_LMT_BY3 = dataType.GetField("LMT_BY3");
            field_LMY_BY1_NV = dataType.GetField("LMY_BY1_NV");
            field_LMY_BY2_NV = dataType.GetField("LMY_BY2_NV");
            field_LMY_BY3_NV = dataType.GetField("LMY_BY3_NV");
            field_LMT_SL1 = dataType.GetField("LMT_SL1");
            field_LMT_SL2 = dataType.GetField("LMT_SL2");
            field_LMT_SL3 = dataType.GetField("LMT_SL3");
            field_LMY_SL1_NV = dataType.GetField("LMY_SL1_NV");
            field_LMY_SL2_NV = dataType.GetField("LMY_SL2_NV");
            field_LMY_SL3_NV = dataType.GetField("LMY_SL3_NV");
            field_LST_DL_PR = dataType.GetField("LST_DL_PR");
            field_LST_DL_VL = dataType.GetField("LST_DL_VL");
            field_DAY_VL = dataType.GetField("DAY_VL");
            field_DAY_DIL_NO = dataType.GetField("DAY_DIL_NO");

            // Market depth (size of the order book) is 3 on TASE
            // i am going to reuse this object 
            marketData = new MarketSimulation.MarketData(3);
            securities = new System.Collections.Hashtable(100);
        }

		/// <summary>
		/// The meethod belongs to the IConsumer interface and called from
		/// data genrerator, like MaofDataGeneratorLogFile
		/// </summary>
        public void Notify(int count, K300MaofType data)
        {
            // convert TASE format to the internal object
            // same object is reused here
            RawDataToMarketData(data, ref marketData);

            // forward to the market simulation logic
            base.Notify(count, marketData);
        }

        /// <summary>
        /// DataType is something like K300MaofType - lot of strings. The method will  convert
        /// this into something convenient to work with.
        /// </summary>
        /// <param name="dt">
        /// A <see cref="System.Object"/>
        /// Object of type DataType
        /// </param>
        /// <returns>
        /// A <see cref="MarketData"/>
        /// New object containing integers like Price, best bid/ask, etc.
        /// </returns>
        protected void RawDataToMarketData(K300MaofType dt, ref MarketSimulation.MarketData md)
        {
            // get ID (unique integeder)
            int securityId = JQuant.Convert.StrToInt((string)field_BNO_Num.GetValue(dt));

            // update local hash table - i keep the latest entry
            securities[securityId] = dt;
            
            md.id = securityId;
            md.bid[0].price = JQuant.Convert.StrToInt((string)field_LMT_BY1.GetValue(dt));
            md.bid[1].price = JQuant.Convert.StrToInt((string)field_LMT_BY2.GetValue(dt));
            md.bid[2].price = JQuant.Convert.StrToInt((string)field_LMT_BY3.GetValue(dt));
            md.bid[0].size = JQuant.Convert.StrToInt((string)field_LMY_BY1_NV.GetValue(dt));
            md.bid[1].size = JQuant.Convert.StrToInt((string)field_LMY_BY2_NV.GetValue(dt));
            md.bid[2].size = JQuant.Convert.StrToInt((string)field_LMY_BY3_NV.GetValue(dt));
            md.ask[0].price = JQuant.Convert.StrToInt((string)field_LMT_SL1.GetValue(dt));
            md.ask[1].price = JQuant.Convert.StrToInt((string)field_LMT_SL2.GetValue(dt));
            md.ask[2].price = JQuant.Convert.StrToInt((string)field_LMT_SL3.GetValue(dt));
            md.ask[0].size = JQuant.Convert.StrToInt((string)field_LMY_SL1_NV.GetValue(dt));
            md.ask[1].size = JQuant.Convert.StrToInt((string)field_LMY_SL2_NV.GetValue(dt));
            md.ask[2].size = JQuant.Convert.StrToInt((string)field_LMY_SL3_NV.GetValue(dt));
            md.lastTrade = JQuant.Convert.StrToInt((string)field_LST_DL_PR.GetValue(dt));
            md.lastTradeSize = JQuant.Convert.StrToInt((string)field_LST_DL_VL.GetValue(dt));
            md.dayVolume = JQuant.Convert.StrToInt((string)field_DAY_VL.GetValue(dt));
            //md.dayTransactions = JQuant.Convert.StrToInt((string)field_DAY_DIL_NO.GetValue(dt));

        }

		/// <summary>
		/// Use this class to get access to the all getters for the Maof option 
		/// Method MarketSimulationMaof.GetSecurity() conveniently creates objects 
		/// of this class
		/// </summary>
		public class Option
		{
			public Option(int id, MarketSimulationMaof ms)
			{
                object o = ms.securities[id];
                if (o != null)
                {
					exists = true;
                    md = (K300MaofType)o;
				}
				else
				{
					exists = false;
				}
					
			}
			
			public bool Exists()
			{
				return exists;
			}
			
	        public string GetName()
	        {
	            string res = "Unknown";
	            
	            if (exists)
	            {
	                res = md.Symbol;
	            }
	
	            return res;
	        }
			
			public int GetExpirationPrice()
			{
	            int res = 0;
	            
	            if (exists)
	            {
	                res = JQuant.Convert.StrToInt(md.EX_PRC, 0);
	            }
	
	            return res;
			}
			
			public bool IsPut()
			{
	            bool res = false;
	            
	            if (exists)
	            {
	                string bno_name = md.BNO_NAME;
					char firstChar = bno_name[0];
					res = (firstChar == 'P');
	            }
	
	            return res;
			}
			
			/// <summary>
			/// Returns best bids or asks
			/// </summary>
			public MarketSimulation.OrderPair[] GetBook(TransactionType transaction)
			{
	            MarketSimulation.OrderPair[] res;
				
				if (transaction == TransactionType.SELL)
				{
					res = GetBookAsk();
				}
				else
				{
					res = GetBookBid();
				}
	            
	
	            return res;
			}
			
			/// <summary>
			/// Returns order book
			/// </summary>
			public MarketSimulation.OrderPair[] GetBookBid()
			{
	            MarketSimulation.OrderPair[] res = new MarketSimulation.OrderPair[0];
	            
	            if (exists)
	            {
					// i have three best bids (three best buy orders)
					res = new MarketSimulation.OrderPair[3];
					res[0] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_BY1, 0), JQuant.Convert.StrToInt(md.LMY_BY1_NV, 0));
					res[1] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_BY2, 0), JQuant.Convert.StrToInt(md.LMY_BY2_NV, 0));
					res[2] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_BY3, 0), JQuant.Convert.StrToInt(md.LMY_BY3_NV, 0));
	            }
	
	            return res;
			}
			
			/// <summary>
			/// Returns order book
			/// </summary>
			public MarketSimulation.OrderPair[] GetBookAsk()
			{
	            MarketSimulation.OrderPair[] res = new MarketSimulation.OrderPair[0];
	            
	            if (exists)
	            {
					// i have three best bids (three best buy orders)
					res = new MarketSimulation.OrderPair[3];
					res[0] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_SL1, 0), JQuant.Convert.StrToInt(md.LMY_SL1_NV, 0));
					res[1] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_SL2, 0), JQuant.Convert.StrToInt(md.LMY_SL2_NV, 0));
					res[2] = new MarketSimulation.OrderPair(
					           JQuant.Convert.StrToInt(md.LMT_SL3, 0), JQuant.Convert.StrToInt(md.LMY_SL3_NV, 0));
	            }
	
	            return res;
			}
			
			private K300MaofType md;
			private bool exists;
		}
		
        /// <summary>
        /// Returns entry from the hashtable keeping the latest market snapshot
        /// </summary>
        public Option GetOption(int id)
        {
            return new Option(id, this);
        }


        protected FieldInfo field_BNO_Num;
        protected FieldInfo field_LMT_BY1;
        protected FieldInfo field_LMT_BY2;
        protected FieldInfo field_LMT_BY3;
        protected FieldInfo field_LMY_BY1_NV;
        protected FieldInfo field_LMY_BY2_NV;
        protected FieldInfo field_LMY_BY3_NV;
        protected FieldInfo field_LMT_SL1;
        protected FieldInfo field_LMT_SL2;
        protected FieldInfo field_LMT_SL3;
        protected FieldInfo field_LMY_SL1_NV;
        protected FieldInfo field_LMY_SL2_NV;
        protected FieldInfo field_LMY_SL3_NV;
        protected FieldInfo field_LST_DL_PR;
        protected FieldInfo field_LST_DL_VL;
        protected FieldInfo field_DAY_VL;
        protected FieldInfo field_DAY_DIL_NO;


        protected MarketSimulation.MarketData marketData;

        /// <summary>
        /// Collection of all traded symbols (different BNO_Num for TASE)
        /// I keep the last update data in this hash table
        /// </summary>
        protected new System.Collections.Hashtable securities;
    }
}
