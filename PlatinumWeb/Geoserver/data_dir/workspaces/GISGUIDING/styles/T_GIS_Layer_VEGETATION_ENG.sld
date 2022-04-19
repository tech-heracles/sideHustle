<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>T_GIS_Layer_VEGETATION</Name>
<UserStyle>
<Name>T_GIS_Layer_VEGETATION</Name>
  
<FeatureTypeStyle>
    <Name>VEGJETACIONI_ENG</Name>
  <Rule>
      <Name>Continuous urban fabric</Name>
      <Title>Continuous urban fabric</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-000-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6004d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
    <Rule>
      <Name>Discontinuous urban fabric</Name>
      <Title>Discontinuous urban fabric</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-000-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ff0000</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Industrial or commercial units</Name>
      <Title>Industrial or commercial units</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-077-242</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#cc4df2</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Port areas</Name>
      <Title>Port areas</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-204-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6cccc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Airports</Name>
      <Title>Airports</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-204-230</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6cce6</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Mineral extraction sites</Name>
      <Title>Mineral extraction sites</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-000-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a600cc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Dump sites</Name>
      <Title>Dump sites</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-077-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a64d00</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Green urban areas</Name>
      <Title>Green urban areas</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-166-255</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffa6ff</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Sport and leisure facilities</Name>
      <Title>Sport and leisure facilities</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-230-255</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffe6ff</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Non-irrigated arable land</Name>
      <Title>Non-irrigated arable land</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-255-168</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffffa8</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Permanently irrigated land</Name>
      <Title>Permanently irrigated land</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-255-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffff00</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Vineyards</Name>
      <Title>Vineyards</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-128-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e68000</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Fruit trees and berry plantations</Name>
      <Title>Fruit trees and berry plantations</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>242-166-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#f2a64d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Olive groves</Name>
      <Title>Olive groves</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-166-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6a600</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Pastures</Name>
      <Title>Pastures</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-230-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6e64d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Complex cultivation patterns</Name>
      <Title>Complex cultivation patterns</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>255-230-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffe64d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Land principally occupied by agriculture, with significant areas of natural vegetation</Name>
      <Title>Land principally occupied by agriculture, with significant areas of natural vegetation</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-204-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6cc4d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Broad-leaved forest</Name>
      <Title>Broad-leaved forest</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>128-255-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#80ff00</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Coniferous forest</Name>
      <Title>Coniferous forest</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>000-166-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#00a600</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Mixed forest</Name>
      <Title>Mixed forest</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>077-255-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#4dff00</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Natural grasslands</Name>
      <Title>Natural grasslands</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-204-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#cccccc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Natural grasslands</Name>
      <Title>Natural grasslands</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-242-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ccf24d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Moors and heathland</Name>
      <Title>Moors and heathland</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-255-128</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6ff80</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Sclerophyllous vegetation</Name>
      <Title>Sclerophyllous vegetation</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-230-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6e64d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Transitional woodland-shrub</Name>
      <Title>Transitional woodland-shrub</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-242-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6f200</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Transitional woodland-shrub</Name>
      <Title>Transitional woodland-shrub</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-242-077</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ccf24d</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Beaches, dunes, sands</Name>
      <Title>Beaches, dunes, sands</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-230-230</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6e6e6</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Bare rocks</Name>
      <Title>Bare rocks</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-204-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#cccccc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Bare rocks</Name>
      <Title>Bare rocks</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-255-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ccffcc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Sparsely vegetated areas</Name>
      <Title>Sparsely vegetated areas</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-242-000</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6f200</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Sparsely vegetated areas</Name>
      <Title>Sparsely vegetated areas</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-255-204</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ccffcc</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Inland marshes</Name>
      <Title>Inland marshes</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-166-255</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6a6ff</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Salt marshes</Name>
      <Title>Salt marshes</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>204-204-255</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ccccff</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Salines</Name>
      <Title>Salines</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>230-230-255</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e6e6ff</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Water courses</Name>
      <Title>Water courses</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>000-204-242</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#00ccf2</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Water bodies</Name>
      <Title>Water bodies</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>128-242-230</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#80f2e6</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Coastal lagoons</Name>
      <Title>Coastal lagoons</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>000-255-166</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#00ffa6</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
  <Rule>
      <Name>Estuaries</Name>
      <Title>Estuaries</Title>      
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>RGB</ogc:PropertyName><ogc:Literal>166-255-230</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#a6ffe6</CssParameter><CssParameter name="fill-opacity">0.6</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#797979</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>  
  </Rule>
  
    
    <Rule>
      <MaxScaleDenominator>34200</MaxScaleDenominator>
      <TextSymbolizer>
        <Geometry>
          <ogc:Function name="centroid">
            <ogc:PropertyName>the_geom</ogc:PropertyName>
          </ogc:Function>
        </Geometry>
        <Label>
          <ogc:PropertyName>LABEL1</ogc:PropertyName>
        </Label>
        <Font>
          <CssParameter name="font-family">Arial</CssParameter>
          <CssParameter name="font-size">11</CssParameter>
          <CssParameter name="font-style">normal</CssParameter>
          <CssParameter name="font-weight">bold</CssParameter>
        </Font>
        <LabelPlacement>
          <PointPlacement>
            <AnchorPoint><AnchorPointX>0.5</AnchorPointX><AnchorPointY>0.0</AnchorPointY></AnchorPoint>
            <Displacement><DisplacementX>0</DisplacementX><DisplacementY>5</DisplacementY></Displacement>
            <Rotation><ogc:Literal>0</ogc:Literal></Rotation>
          </PointPlacement>
        </LabelPlacement>
        <Halo>
          <Radius>
            <ogc:Literal>1.0</ogc:Literal>
          </Radius>
          <Fill>
            <CssParameter name="fill">#FFFFFF</CssParameter>
          </Fill>
        </Halo>
        <VendorOption name="conflictResolution">true</VendorOption>
        <VendorOption name="goodnessOfFit">0</VendorOption>
        <VendorOption name="autoWrap">60</VendorOption>
      </TextSymbolizer>
    </Rule>
  
</FeatureTypeStyle> 
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>