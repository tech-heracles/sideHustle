<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<NamedLayer>
  <Name>V_GIS_Layer_DPSH_RASTEEVIDENTUAR</Name>
  <UserStyle>
	<Name>V_GIS_Layer_DPSH_RASTEEVIDENTUAR</Name>
	<Title>V_GIS_Layer_DPSH_RASTEEVIDENTUAR</Title>
	<Abstract>V_GIS_Layer_DPSH_RASTEEVIDENTUAR</Abstract>

	<FeatureTypeStyle> 
	<Rule>
	<Name>  0%</Name>     
	  <ogc:Filter>
		<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
	  </ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#e7f3f7</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#b7dbe7</CssParameter><CssParameter name="fill-opacity">0.5</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
	<Rule>
      
	<Name>  0-5%</Name>        
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>5</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#b7dbe7</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#88c4d8</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
	
	<Rule>
	<Name>  5-10%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>5</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>10</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#88c4d8</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#58adc9</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  
       
	<Rule>
	  <Name>  10-15%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>10</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>15</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#58adc9</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#118ab2</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>  

	<Rule>
	  <Name>  15-20%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>15</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>20</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#118ab2</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#0d6e8e</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>     

	<Rule>
	  <Name>  20-30%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>20</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>30</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#0d6e8e</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#0a526a</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>       

	<Rule>
	  <Name>  30-50%</Name>     
		<ogc:Filter>
		  <ogc:And>
			<ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>30</ogc:Literal></ogc:PropertyIsGreaterThan>
			<ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>50</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
		  </ogc:And>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#0a526a</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#052935</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>     
 

	<Rule>
	  <Name>  50-100%</Name>     
		<ogc:Filter>
		  <ogc:PropertyIsGreaterThan><ogc:PropertyName>PERQ</ogc:PropertyName><ogc:Literal>50</ogc:Literal></ogc:PropertyIsGreaterThan>
		</ogc:Filter>
	  <PolygonSymbolizer>
        <Fill><CssParameter name="fill">#052935</CssParameter><CssParameter name="fill-opacity">0.9</CssParameter></Fill>
        <Stroke><CssParameter name="stroke">#010d11</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
      </PolygonSymbolizer>	
	</Rule>     

	<!-- Teksti per elementet e tjere -->    
      
	<Rule>
      <MaxScaleDenominator>300000</MaxScaleDenominator>
      <TextSymbolizer>
        <Geometry>
            <ogc:PropertyName>the_geom</ogc:PropertyName>
        </Geometry>
        <Label>
          <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><![CDATA[
       ]]> Nr:<ogc:PropertyName>VLERA</ogc:PropertyName><![CDATA[
       ]]> <ogc:PropertyName>PERQ</ogc:PropertyName>(%)
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
		  <CssParameter name="fill">#031b23</CssParameter>
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