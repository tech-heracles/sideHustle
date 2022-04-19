<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<NamedLayer>
  <Name>V_GIS_Layer_MBETJE_TARIFFCOLLECTION</Name>
  <UserStyle>
	<Name>V_GIS_Layer_MBETJE_TARIFFCOLLECTION</Name>
	<Title>V_GIS_Layer_MBETJE_TARIFFCOLLECTION</Title>
	<Abstract>V_GIS_Layer_MBETJE_TARIFFCOLLECTION</Abstract>

	<FeatureTypeStyle> 
	<Rule>
	<Name>  0-40%</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>40</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ff1a1a</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  

	<Rule>
	  <Name>  41-75%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>41</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>75</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#ffff00</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>     

	<Rule>
	  <Name>  76-100%</Name>     
		<ogc:Filter>
		  <ogc:PropertyIsGreaterThan><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>75</ogc:Literal></ogc:PropertyIsGreaterThan>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#00cc00</CssParameter><CssParameter name="fill-opacity">0.7</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#7b3e00</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>     

	<!-- Teksti per elementet e tjere -->    
      
	<Rule>
      <TextSymbolizer>
        <Geometry>
            <ogc:PropertyName>the_geom</ogc:PropertyName>
        </Geometry>
        <Label>
          <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><![CDATA[
       ]]> <ogc:PropertyName>VLERA</ogc:PropertyName>%
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