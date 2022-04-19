<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>V_GIS_Layer_NYJE_NR2_ENG</Name>
    <UserStyle>
      <Name>V_GIS_Layer_NYJE_NR2_ENG</Name>
      <Title>V_GIS_Layer_NYJE_NR2_ENG</Title>
      <Abstract>V_GIS_Layer_NYJE_NR2_ENG</Abstract>

      <FeatureTypeStyle>

        <Name>Splice</Name>
        <Rule>
          <Name>Splice</Name>
          <Title>Splice</Title>
          <MaxScaleDenominator>1100</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple" xlink:href="LegendIMG/nyjeNR2.png"/>
                <Format>image/PNG</Format>
              </ExternalGraphic>
              <Size>15</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>

        <Rule>
          <MinScaleDenominator>1100</MinScaleDenominator>
          <MaxScaleDenominator>4300</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <Mark>
                <WellKnownName>circle</WellKnownName>
                <Fill>
                  <CssParameter name="fill">#815226</CssParameter>
                </Fill>
                <Stroke>
                  <CssParameter name="stroke">#ffffff</CssParameter>
                </Stroke>
              </Mark>
              <Size>5</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>