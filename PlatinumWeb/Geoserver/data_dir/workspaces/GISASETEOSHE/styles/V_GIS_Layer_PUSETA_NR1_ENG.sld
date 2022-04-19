<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>V_GIS_Layer_PUSETA_NR1_ENG</Name>
    <UserStyle>
      <Name>V_GIS_Layer_PUSETA_NR1_ENG</Name>
      <Title>V_GIS_Layer_PUSETA_NR1_ENG</Title>
      <Abstract>V_GIS_Layer_PUSETA_NR1_ENG</Abstract>

      <FeatureTypeStyle>

        <Name>Manhole</Name>
        <Rule>
          <Name>Manhole</Name>
          <Title>Manhole</Title>
          <MaxScaleDenominator>2300</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple" xlink:href="LegendIMG/pusetaNR1.png"/>
                <Format>image/PNG</Format>
              </ExternalGraphic>
              <Size>20</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>

        <Rule>
          <MinScaleDenominator>2300</MinScaleDenominator>
          <MaxScaleDenominator>9000</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <Mark>
                <WellKnownName>circle</WellKnownName>
                <Fill>
                  <CssParameter name="fill">#00ff00</CssParameter>
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