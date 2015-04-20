### Introduction ###
I need to get TA 25 index data, specifically its base price. Base price is the previous trading day adjusted close, vs which are made all the index computations for the current trading day.

### Details ###

In the newer TaskBar versions there is K300Class method called GetBaseAssets2, which replace GetBaseAssets (surprise!). It returns an array of BaseAssetType objects. The field 'Value' is what I seek.