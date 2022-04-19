<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>MZHU_Venddepozitime_AL</Name>
<UserStyle>
<Name>MZHU_Venddepozitime_AL</Name>
  
<FeatureTypeStyle>
    <Name>Venddepozimet</Name>
  <Rule>
      <Name>Sanitare</Name>
      <Title>Venddepozimet Sanitare</Title>      
      <ogc:Filter>
    <ogc:PropertyIsEqualTo><ogc:PropertyName>EshteSanitar</ogc:PropertyName><ogc:Literal>SANITAR</ogc:Literal></ogc:PropertyIsEqualTo>
    </ogc:Filter>
    
      <PolygonSymbolizer>
        <Fill>
          <sld:CssParameter name="fill">#138F6A</sld:CssParameter>
          <sld:CssParameter name="fill-opacity">0.76</sld:CssParameter>
        </Fill>
        <Stroke>
          <sld:CssParameter name="stroke">#04421F</sld:CssParameter>
          <sld:CssParameter name="stroke-opacity">0.3</sld:CssParameter>
        </Stroke>
      </PolygonSymbolizer>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/mbetjetSan.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>
  
    <Rule>
      <Name>Sanitare</Name>
      <Title>Venddepozimet JoSanitare</Title>      
      <ogc:Filter>
    <ogc:PropertyIsEqualTo><ogc:PropertyName>EshteSanitar</ogc:PropertyName><ogc:Literal>JO SANITAR</ogc:Literal></ogc:PropertyIsEqualTo>
    </ogc:Filter>
    
      <PolygonSymbolizer>
        <Fill>
          <sld:CssParameter name="fill">#E2817F</sld:CssParameter>
          <sld:CssParameter name="fill-opacity">0.76</sld:CssParameter>
        </Fill>
        <Stroke>
          <sld:CssParameter name="stroke">#B22546</sld:CssParameter>
          <sld:CssParameter name="stroke-opacity">0.3</sld:CssParameter>
        </Stroke>
      </PolygonSymbolizer>  
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/mbetjetJosan.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>
  
    <Rule>
      <MaxScaleDenominator>250000</MaxScaleDenominator>
      <TextSymbolizer>
        <Geometry>
          <ogc:Function name="centroid">
            <ogc:PropertyName>the_geom</ogc:PropertyName>
          </ogc:Function>
        </Geometry>
        <Label>
          <ogc:PropertyName>Bashkia</ogc:PropertyName>
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