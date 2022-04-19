<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<NamedLayer>
  <Name>V_GIS_Layer_MBETJE_MENYRESHERBIMI</Name>
  <UserStyle>
	<Name>V_GIS_Layer_MBETJE_MENYRESHERBIMI</Name>
	<Title>V_GIS_Layer_MBETJE_MENYRESHERBIMI</Title>
	<Abstract>V_GIS_Layer_MBETJE_MENYRESHERBIMI</Abstract>

	<FeatureTypeStyle> 
	<Rule>
	<Name>  Publik</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>1</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#CC3333</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>
	</Rule> 
    
    <Rule>
	<Name>  Privat</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>2</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#008AB8</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
    
    <Rule>
	<Name>  PPP</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>3</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#279B61</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
    
    <Rule>
	<Name>  Publike, Privat</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>4</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#FFCC33</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule> 
    
    <Rule>
	<Name>  Publike, PPP</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>5</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#CC6699</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule> 
    
    <Rule>
	<Name>  Private, PPP</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>6</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#3F5D7D</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
    
    <Rule>
	<Name>  Publike, Privat, PPP</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>7</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#A3E496</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule> 
    
    <Rule>
	<Name></Name>     
	  <ogc:Filter>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>8</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">0.2</CssParameter></Stroke>
      </PolygonSymbolizer>	
	  <PolygonSymbolizer>
        <Stroke><CssParameter name="stroke">#FFFF7A</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  

	<!-- Teksti per elementet e tjere -->    
      
	<Rule>
      <TextSymbolizer>
        <Geometry>
            <ogc:PropertyName>the_geom</ogc:PropertyName>
        </Geometry>
        <Label>
          <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName>
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
		<Fill>
		  <CssParameter name="fill">#00688B</CssParameter>
		</Fill>
        <VendorOption name="conflictResolution">true</VendorOption>
        <VendorOption name="goodnessOfFit">0</VendorOption>
        <VendorOption name="autoWrap">150</VendorOption>
      </TextSymbolizer>
    </Rule>
	
	</FeatureTypeStyle>

	</UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>