<?xml version="1.0" encoding="UTF-8"?><sld:StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" version="1.0.0">
  <sld:NamedLayer>
    <sld:Name>V_GIS_Layer_MZHULANDFILLS_ENG</sld:Name>
    <sld:UserStyle>
      <sld:Name>V_GIS_Layer_MZHULANDFILLS_ENG</sld:Name>
      <sld:Title>V_GIS_Layer_MZHULANDFILLS_ENG</sld:Title>
      <sld:FeatureTypeStyle>
      <sld:Name>V_GIS_Layer_MZHULANDFILLS_ENG</sld:Name>
        
       <sld:Rule>
          <sld:Name>LANDFILLS</sld:Name>
          <sld:Title>SANITARY</sld:Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>LLOJI</ogc:PropertyName>
              <ogc:Literal>SANITAR</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <sld:PointSymbolizer>
            <sld:Graphic>
              <sld:ExternalGraphic>
                <sld:OnlineResource xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple" xlink:href="LegendIMG/mbetjetSan.PNG"/>
                <sld:Format>image/PNG</sld:Format>
              </sld:ExternalGraphic>
              <sld:Size>20</sld:Size>
            </sld:Graphic>
          </sld:PointSymbolizer>
        </sld:Rule>
        
		<sld:Rule>
          <sld:Name>LANDFILLS</sld:Name>
          <sld:Title>UNSANITARY</sld:Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>LLOJI</ogc:PropertyName>
              <ogc:Literal>JO SANITAR</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <sld:PointSymbolizer>
            <sld:Graphic>
              <sld:ExternalGraphic>
                <sld:OnlineResource xmlns:xlink="http://www.w3.org/1999/xlink" xlink:type="simple" xlink:href="LegendIMG/mbetjetJosan.PNG"/>
                <sld:Format>image/PNG</sld:Format>
              </sld:ExternalGraphic>
              <sld:Size>20</sld:Size>
            </sld:Graphic>
          </sld:PointSymbolizer>
		</sld:Rule>        
        
      </sld:FeatureTypeStyle>
    </sld:UserStyle>
  </sld:NamedLayer>
</sld:StyledLayerDescriptor>