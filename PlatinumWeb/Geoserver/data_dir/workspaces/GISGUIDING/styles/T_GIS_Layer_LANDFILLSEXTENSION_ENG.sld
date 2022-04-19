<?xml version="1.0" encoding="UTF-8"?><sld:StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" version="1.0.0">
  <sld:NamedLayer>
    <sld:Name>T_GIS_Layer_LANDFILLSEXTENSION</sld:Name>
    <sld:UserStyle>
      <sld:Name>T_GIS_Layer_LANDFILLSEXTENSION</sld:Name>
      <sld:Title>Default Polygon</sld:Title>
      <sld:Abstract>A sample style that draws a polygon</sld:Abstract>
      <sld:FeatureTypeStyle>
        <sld:Name>T_GIS_Layer_LANDFILLSEXTENSION</sld:Name>
        
        <sld:Rule>
          <sld:Name>LANDFILLSEXTENSION1</sld:Name>
          <sld:Title>SANITARY</sld:Title>
          <sld:Abstract>A polygon with a gray fill and a 1 pixel black outline</sld:Abstract>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>ESHTESANITAR</ogc:PropertyName>
              <ogc:Literal>SANITAR</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <sld:PolygonSymbolizer>
            <sld:Fill>
              <sld:CssParameter name="fill">#138F6A</sld:CssParameter>
              <sld:CssParameter name="fill-opacity">0.76</sld:CssParameter>
            </sld:Fill>
            <sld:Stroke>
              <sld:CssParameter name="stroke">#04421F</sld:CssParameter>
              <sld:CssParameter name="stroke-opacity">0.3</sld:CssParameter>
            </sld:Stroke>
          </sld:PolygonSymbolizer>
        </sld:Rule>
        
        <sld:Rule>
          <sld:Name>LANDFILLSEXTENSION2</sld:Name>
          <sld:Title>UNSANITARY</sld:Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>ESHTESANITAR</ogc:PropertyName>
              <ogc:Literal>JO SANITAR</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <sld:PolygonSymbolizer>
            <sld:Fill>
              <sld:CssParameter name="fill">#E2817F</sld:CssParameter>
            </sld:Fill>
            <sld:Stroke>
              <sld:CssParameter name="stroke">#B22546</sld:CssParameter>
              <sld:CssParameter name="stroke-opacity">0.3</sld:CssParameter>
            </sld:Stroke>
          </sld:PolygonSymbolizer>
        </sld:Rule>
        
        <sld:Rule>
          <sld:Name>LANDFILLSEXTENSION3</sld:Name>
          <sld:Title>UNSPECIFIED</sld:Title>
          <ogc:Filter>
            <ogc:PropertyIsNull>
              <ogc:PropertyName>ESHTESANITAR</ogc:PropertyName>            
            </ogc:PropertyIsNull>
          </ogc:Filter>
          <sld:PolygonSymbolizer>
            <sld:Fill>
              <sld:CssParameter name="fill">#000000</sld:CssParameter>
            </sld:Fill>
            <sld:Stroke>
              <sld:CssParameter name="stroke">#000000</sld:CssParameter>
              <sld:CssParameter name="stroke-opacity">0.3</sld:CssParameter>
            </sld:Stroke>
          </sld:PolygonSymbolizer>
        </sld:Rule>
        
      </sld:FeatureTypeStyle>
    </sld:UserStyle>
  </sld:NamedLayer>
</sld:StyledLayerDescriptor>