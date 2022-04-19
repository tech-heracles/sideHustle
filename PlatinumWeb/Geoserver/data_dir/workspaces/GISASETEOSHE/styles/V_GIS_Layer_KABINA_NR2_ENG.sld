<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_KABINA_NR2_ENG</Name>
<UserStyle>
<Name>V_GIS_Layer_KABINA_NR2_ENG</Name>
	
<FeatureTypeStyle>
    <Name>Cabin</Name>
	
	<Rule>
      <Name>Cabin</Name>
      <Title>Cabin</Title>  
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Name>Cabin</Name>
      <Title>Cabin</Title>  
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	
		
   <Rule>
      <Name>Cabin</Name>
      <Title>Cabin</Title>      
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Cabin</Name>
      <Title>Cabin</Title>      
      <MinScaleDenominator>1100</MinScaleDenominator>
    <MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Cabin</Name>
      <Title>Cabin</Title>      
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaNR2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
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
             <CssParameter name="font-size">10</CssParameter>
             <CssParameter name="font-style">normal</CssParameter>
             <CssParameter name="font-weight">bold</CssParameter>
           </Font>
           <LabelPlacement>
             <PointPlacement>
              <AnchorPoint>
                 <AnchorPointX>
                   0.5
             	 </AnchorPointX>
               	 <AnchorPointY>
                   -1.5
              	 </AnchorPointY>
              </AnchorPoint> 
            </PointPlacement>
           </LabelPlacement>
                      
           <Halo>
            <Radius>
              <ogc:Literal>1.5</ogc:Literal>
            </Radius>
			<Fill>
              <CssParameter name="fill">#FFFFFF</CssParameter>
            </Fill>
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