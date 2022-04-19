<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>V_GIS_Layer_LINJE6</Name>
    <UserStyle>
      <Name>V_GIS_Layer_LINJE6</Name>
      <Title>V_GIS_Layer_LINJE6</Title>
      <Abstract>V_GIS_Layer_LINJE6</Abstract>

      <FeatureTypeStyle>
        <Name>Linje</Name>
        <Rule>
          <Name></Name>
          <Title></Title>
          <Abstract></Abstract>
          <MaxScaleDenominator>17000</MaxScaleDenominator>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#5ddfff</CssParameter>
              <CssParameter name="stroke-width">1.5</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter>
              <CssParameter name="stroke-linecap">round</CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>
        
        <Rule>
          <Name></Name>
          <Title></Title>
          <Abstract></Abstract>
          <MinScaleDenominator>17000</MinScaleDenominator>
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#5ddfff</CssParameter>
              <CssParameter name="stroke-width">0.8</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter>
              <CssParameter name="stroke-linecap">round</CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>