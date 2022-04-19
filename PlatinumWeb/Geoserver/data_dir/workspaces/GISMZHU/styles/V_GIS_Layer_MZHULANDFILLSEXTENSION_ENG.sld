<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <NamedLayer>
    <Name>V_GIS_Layer_MZHULANDFILLSEXTENSION_ENG</Name>
    <UserStyle>
      <Title>A azure polygon style</Title>
      <FeatureTypeStyle>
        
        <Rule>
          <Title>SANITARY</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>SANITAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#138F6A</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#04421F</CssParameter>
              <CssParameter name="stroke-width">0.3</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        
        <Rule>
          <Title>UNSANITARY</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>JO SANITAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#E2817F</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#B22546</CssParameter>
              <CssParameter name="stroke-width">0.3</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        
        <Rule>
          <Title>UNSPECIFIED</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#000000</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#000000</CssParameter>
              <CssParameter name="stroke-width">0.3</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>