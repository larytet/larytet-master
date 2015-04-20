Some examples are here [JQuant/www/index\_local.html](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/www/index_local.html)

  * Dialog box with HTML table
  * Dynamic data in the dojox.drid
  * Background color of the row in the dojox.grid
  * Lot of examples of work with JSON

# Menu #

```

function InitMenu() {
pMenuBar = new dijit.MenuBar({});

var pSubMenu = new dijit.Menu({});
pSubMenu.addChild(new dijit.MenuItem({
label: "Status"
}));
pSubMenu.addChild(new dijit.MenuItem({
label: "Counters"
}));
pMenuBar.addChild(new dijit.PopupMenuBarItem({
label: "Info",
popup: pSubMenu
}));

var pSubMenu2 = new dijit.Menu({});
pSubMenu2.addChild(new dijit.MenuItem({
label: "Connection"
}));
pSubMenu2.addChild(new dijit.MenuItem({
label: "Semiautomatic"
}));
pMenuBar.addChild(new dijit.PopupMenuBarItem({
label: "Trade",
popup: pSubMenu2
}));

// add the menu to the top "panel"
pMenuBar.placeAt("toppanel");
// and start the whole thing
pMenuBar.startup();
};
```

```

function InitMenu() {
pMenuBar = new dijit.MenuBar({});

pMenuBar.addChild(new dijit.MenuBarItem({
label: "Info",
}));


// add the menu to the top "panel"
pMenuBar.placeAt("toppanel");
// and start the whole thing
pMenuBar.startup();
};
```

# Charting #

```

function CreateTrafficChart() {
SeriesBytesIn = new Array();
SeriesBytesOut = new Array();

var chart = new dojox.charting.Chart2D("simplechart");

chart.addPlot("default", {type: "Lines", markers: false,
tension:3, hAxis: "t", vAxis: "kB/s"});
chart.addAxis("t", {fixUpper: "major", fixLower:"minor", min: 0});
chart.addAxis("kB/s", {vertical: true, fixUpper: "major", min: 500});

HttpChartAddSeries(chart, [0], [0]);

return chart;
};

function HttpChartAddSeries(chart, seriesIn, seriesOut)
{
chart.addSeries("BytesIn", seriesIn, {stroke: {color: "red", width: 2}, fill: "red"});
chart.addSeries("BytesOut", seriesOut, {stroke: {color: "orange", width: 2}, fill: "orange"});
}

function HttpChartRemoveSeries(chart)
{
chart.removeSeries("BytesIn");
chart.removeSeries("BytesOut");
}

function HttpChartUpdateSeries(chart, seriesIn, seriesOut)
{
chart.updateSeries("BytesIn", seriesIn);
chart.updateSeries("BytesOut", seriesOut);
}

function UpdateHttpChart(chart, bytesIn, bytesOut) {

SeriesBytesIn.push(bytesIn);
SeriesBytesOut.push(bytesOut);

while (SeriesBytesIn.length > 10) SeriesBytesIn.shift();
while (SeriesBytesOut.length > 10) SeriesBytesOut.shift();

// HttpChartRemoveSeries(chart);
HttpChartUpdateSeries(chart, SeriesBytesIn, SeriesBytesOut);
chart.render();
};

<div id="simplechart" style="width: 450px; height: 300px; margin: 5px auto 0px auto;">


Unknown end tag for &lt;/div&gt;


```