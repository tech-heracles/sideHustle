<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>T_GIS_Layer_CULTURALMONUMENTS</Name>
<UserStyle>
<Name>T_GIS_Layer_CULTURALMONUMENTS</Name>
  
<FeatureTypeStyle>
    <Name>Monumentet e Kultures</Name>
  <Rule>
      <Name></Name>
      <Title></Title>      
      <MaxScaleDenominator>17000</MaxScaleDenominator>
      
      <PolygonSymbolizer>
        <Fill>
          <CssParameter name="fill">#7e420c</CssParameter>
          <CssParameter name="fill-opacity">0.3</CssParameter>
        </Fill>
        <Stroke>
          <CssParameter name="stroke">#7e420c</CssParameter>
          <CssParameter name="fill-opacity">1</CssParameter>
        </Stroke>
      </PolygonSymbolizer>  
      
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/muze.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>
  
  <Rule>
      <Name></Name>
      <Title></Title>  
    <MinScaleDenominator>17001</MinScaleDenominator>
    <MaxScaleDenominator>3400000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/muze.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>  
    
    <Rule>
      <MaxScaleDenominator>3400000</MaxScaleDenominator>
      <TextSymbolizer>
        <Geometry>
          <ogc:Function name="centroid">
            <ogc:PropertyName>the_geom</ogc:PropertyName>
          </ogc:Function>
        </Geometry>
        <Label>
          <ogc:PropertyName>EMERTIMI</ogc:PropertyName>
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