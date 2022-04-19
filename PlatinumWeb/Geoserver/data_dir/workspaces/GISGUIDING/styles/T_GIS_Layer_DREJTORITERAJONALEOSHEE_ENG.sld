<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
    xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
    xmlns="http://www.opengis.net/sld" 
    xmlns:ogc="http://www.opengis.net/ogc" 
    xmlns:xlink="http://www.w3.org/1999/xlink" 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  
  <NamedLayer>
    <Name>Drejtorite Rajonale Oshee</Name>
    <UserStyle>      
      <Title>Personalized style For Regional Departments</Title>
      <Abstract>Stili eshte polygon dhe ka ngjryren e zones</Abstract>
      
      <FeatureTypeStyle>
        <Rule>
          <Name>Rule 1</Name>
          <Title>KORCE AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>BEZHË</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#F5DEB3</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule> 
        
         <Rule>
          <Name>Rule 2</Name>
          <Title>SHKODER AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>BOJQELL</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
           <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#42C0FB</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 3</Name>
          <Title>BURREL AREA</Title>           
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>ÇIKLAMIN</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#ff61ff</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 4</Name>
          <Title>TIRANE AREA</Title>           
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>GJELBËR</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#77ea8e</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 5</Name>
          <Title>KUKES AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>LEJLA</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#7a7aff</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 6</Name>
          <Title>GJIROKASTER AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>PORTOKALLI</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#FFA500</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 7</Name>
          <Title>FIER AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>ROZË</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#FF69B4</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
        <Rule>
          <Name>Rule 8</Name>
          <Title>VLORE AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>KUQE</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#FF3030</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 9</Name>
          <Title>DURRES AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>ULLI</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#556B2F</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 10</Name>
          <Title>ELBASAN AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>VERDHË</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#FFFF73</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
         <Rule>
          <Name>Rule 11</Name>
          <Title>BERAT AREA</Title>
          <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>Ngjyra</ogc:PropertyName><ogc:Literal>VIOLË</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#BF5FFF</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#000000</CssParameter><CssParameter name="stroke-width">1</CssParameter></Stroke>
          </PolygonSymbolizer>
        </Rule>
        
        <Rule>        
          <TextSymbolizer>
            <Label><ogc:PropertyName>Pershkrimi</ogc:PropertyName></Label>
            <Font>
              <CssParameter name="font-family">Arial</CssParameter><CssParameter name="font-size">12</CssParameter>
              <CssParameter name="font-style">normal</CssParameter><CssParameter name="font-weight">bold</CssParameter>
            </Font>
            
            <LabelPlacement><PointPlacement><Displacement><DisplacementX>0</DisplacementX><DisplacementY>0</DisplacementY></Displacement></PointPlacement></LabelPlacement>
            
            <Halo>
              <Radius>1.5</Radius>
              <Fill>
                <CssParameter name="fill">#f1f1f1</CssParameter>
              </Fill>
            </Halo>
            
            <Fill><CssParameter name="fill">#222222</CssParameter></Fill>
            
            <VendorOption name="maxDisplacement">50</VendorOption>
            <VendorOption name="autoWrap">100</VendorOption>
            <VendorOption name="conflictResolution">false</VendorOption>
            <VendorOption name="goodnessOfFit">0.1</VendorOption>
            
          </TextSymbolizer>  
        </Rule>

        </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>