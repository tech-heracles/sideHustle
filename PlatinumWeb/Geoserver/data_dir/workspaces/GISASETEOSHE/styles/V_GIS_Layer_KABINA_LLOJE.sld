<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_KABINA_LLOJE</Name>
<UserStyle>
<Name>V_GIS_Layer_KABINA_LLOJE</Name>
	
<FeatureTypeStyle>
    <Name>Kabina</Name>
	
	<!-- Kabina Shtyllore -->
	<Rule>
      <Name> Kabina Shtyllore</Name>
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Ne shtylle</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
	  <MaxScaleDenominator>18000</MaxScaleDenominator>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREeg.png"/><Format>image/PNG</Format>
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
	
	<!-- Kabina Box -->
	<Rule>
      <Name> Kabina Box</Name>
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Parafabrikat</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
	  <MaxScaleDenominator>18000</MaxScaleDenominator>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXeg.png"/><Format>image/PNG</Format>
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
	
	<!-- Kabina Murature -->
    <Rule>
      <Title> Kabina Murature </Title>
      <ogc:Filter><ogc:Or>
		<ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
        <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Brendshme</ogc:Literal></ogc:PropertyIsEqualTo>
	  </ogc:Or></ogc:Filter>  
	  <MaxScaleDenominator>18000</MaxScaleDenominator>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREeg.png"/><Format>image/PNG</Format>
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
	
	  <!-- Gjithe rastet e papercaktuar -->    
	<Rule>
      <Title>Kabine e papercaktuar</Title>
      <ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter>  
	  <MaxScaleDenominator>18000</MaxScaleDenominator>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/><Format>image/PNG</Format>
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
	
	<!-- Teksti -->
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