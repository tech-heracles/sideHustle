<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_KABINA_NR1</Name>
<UserStyle>
<Name>V_GIS_Layer_KABINA_NR1</Name>
	
<FeatureTypeStyle>
    <Name>Kabina</Name>
	
	<Rule>
      <Name></Name>
      <Title></Title>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR1.png"/><Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>
			<ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
				<ogc:Literal>22</ogc:Literal><ogc:Literal>2200</ogc:Literal>
				<ogc:Literal>18</ogc:Literal><ogc:Literal>5000</ogc:Literal>
				<ogc:Literal>12</ogc:Literal><ogc:Literal>10000</ogc:Literal>
				<ogc:Literal>10</ogc:Literal>
			</ogc:Function>
		  </Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>  
  
  	<Rule>
	<MaxScaleDenominator>9000</MaxScaleDenominator>
	<TextSymbolizer>
	  <Label>
		<ogc:PropertyName>PERSHKRIMI</ogc:PropertyName>
	  </Label>
	  <Font>
		 <CssParameter name="font-family">Arial</CssParameter>
		 <CssParameter name="font-style">normal</CssParameter>
		 <CssParameter name="font-weight">bold</CssParameter>
         <CssParameter name="font-size">
			 <ogc:Function name="Categorize">
			   <ogc:Function name="env">
				 <ogc:Literal>wms_scale_denominator</ogc:Literal>
			   </ogc:Function>
			   <ogc:Literal>12</ogc:Literal><ogc:Literal>1000</ogc:Literal>
			   <ogc:Literal>10</ogc:Literal><ogc:Literal>2000</ogc:Literal>
			   <ogc:Literal>8</ogc:Literal>
			 </ogc:Function>
         </CssParameter>
	  </Font>
	  <LabelPlacement>
		 <PointPlacement>
		  <AnchorPoint><AnchorPointX>0.5</AnchorPointX><AnchorPointY>-1.5</AnchorPointY></AnchorPoint>
		</PointPlacement>
	  </LabelPlacement>                      
	  <Halo>
		<Radius><ogc:Literal>1.5</ogc:Literal></Radius>
		<Fill> <CssParameter name="fill">#FFFFFF</CssParameter></Fill>
	  </Halo>
	  <VendorOption name="followLine">true</VendorOption>
	  <VendorOption name="repeat">400</VendorOption>
	  <VendorOption name="spaceAround">10</VendorOption>
	  <VendorOption name="maxAngleDelta">15</VendorOption>
	  <VendorOption name="conflictResolution">true</VendorOption>
	  <VendorOption name="forceLeftToRight">false</VendorOption>
	</TextSymbolizer>
	</Rule>
  
</FeatureTypeStyle> 
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>