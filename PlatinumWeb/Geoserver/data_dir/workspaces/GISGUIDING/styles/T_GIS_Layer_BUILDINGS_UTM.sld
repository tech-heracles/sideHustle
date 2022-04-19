<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
 <NamedLayer>
    <Name>T_GIS_Layer_BUILDINGS_UTM</Name>
    <UserStyle>
      <Name>T_GIS_Layer_BUILDINGS_UTM</Name>
      <Title>T_GIS_Layer_BUILDINGS_UTM</Title>
      <Abstract>A sample style that draws a polygon</Abstract>
      <FeatureTypeStyle>
        <Name>name</Name>
        <Rule>
          <Name>rule1</Name>
          <Title></Title>
          <Abstract>A polygon with a gray fill and a 1 pixel black outline</Abstract>
          <MaxScaleDenominator>17061</MaxScaleDenominator>
          <PolygonSymbolizer>
              <Fill>
              <CssParameter name="fill">#aaaaaa</CssParameter>
              <CssParameter name="fill-opacity">1</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#777777</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>