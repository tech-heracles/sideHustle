<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>T_GIS_Layer_TOPOGRAPHY</Name>
    <UserStyle>
      <Name>T_GIS_Layer_TOPOGRAPHY</Name>
      <Title>T_GIS_Layer_TOPOGRAPHY</Title>
      <Abstract>T_GIS_Layer_TOPOGRAPHY</Abstract>

      <FeatureTypeStyle>
        <Name>TOPOGRAFIA</Name>
        <Rule>
          <Name></Name>
          <Title></Title>
          <Abstract></Abstract>
          <MaxScaleDenominator>8400</MaxScaleDenominator>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#48b63a</CssParameter>
              <CssParameter name="stroke-width">1.6</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter>
              <CssParameter name="stroke-linecap">round</CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>
        
        <Rule>
          <Name></Name>
          <Title></Title>
          <Abstract></Abstract>
          <MinScaleDenominator>8500</MinScaleDenominator>
          <MaxScaleDenominator>69000</MaxScaleDenominator>
          
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#48b63a</CssParameter>
              <CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter>
              <CssParameter name="stroke-linecap">round</CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>
        
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>