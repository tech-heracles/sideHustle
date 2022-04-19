<?xml version="1.0" encoding="UTF-8"?><sld:StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" version="1.0.0">
  <sld:NamedLayer>
    <sld:Name>T_GIS_Layer_KQZ2017_NDERTESAKODE</sld:Name>
    <sld:UserStyle>
      <sld:Name>T_GIS_Layer_KQZ2017_NDERTESAKODE</sld:Name>
      <sld:Title>Kodet e Ndertesave Zgjedhore</sld:Title>
      
      <sld:FeatureTypeStyle>
        <sld:Name>Codi</sld:Name>
        <sld:Rule>
          <sld:Name>default</sld:Name>
          <sld:Title></sld:Title>

         <sld:MaxScaleDenominator>8530</sld:MaxScaleDenominator>
            <sld:PointSymbolizer>
            <sld:Graphic>
              <sld:Mark>
                <sld:WellKnownName>square</sld:WellKnownName>
                <sld:Fill><sld:CssParameter name="fill">#E5E8EC</sld:CssParameter></sld:Fill>
                <sld:Stroke><sld:CssParameter name="stroke">#040404</sld:CssParameter></sld:Stroke>
              </sld:Mark>
              
              <Size>
                <ogc:Function name="Categorize">
                    <ogc:Function name="env">
                      <ogc:Literal>wms_scale_denominator</ogc:Literal>
                    </ogc:Function>
                    <!-- Range [<= 1000]=5, [1000-3000]=4, [3000-5000]=3, and [>5000]=2 -->
                    <ogc:Literal>4</ogc:Literal><ogc:Literal>1000</ogc:Literal>
                    <ogc:Literal>3</ogc:Literal><ogc:Literal>3000</ogc:Literal>
                    <ogc:Literal>2</ogc:Literal><ogc:Literal>5000</ogc:Literal>
                    <ogc:Literal>1</ogc:Literal>
                </ogc:Function>
              </Size>
              
            </sld:Graphic>
          </sld:PointSymbolizer>
      
          <sld:TextSymbolizer>
            <sld:Label>
              <ogc:PropertyName>KODI</ogc:PropertyName>
            </sld:Label>
            <sld:Font>
              <sld:CssParameter name="font-family">Arial</sld:CssParameter>
              <sld:CssParameter name="font-size">
                <ogc:Function name="Categorize">
                    <ogc:Function name="env">
                      <ogc:Literal>wms_scale_denominator</ogc:Literal>
                    </ogc:Function>
                    <ogc:Literal>10</ogc:Literal><ogc:Literal>1000</ogc:Literal>
                    <ogc:Literal>9</ogc:Literal><ogc:Literal>3000</ogc:Literal>
                    <ogc:Literal>8</ogc:Literal><ogc:Literal>5000</ogc:Literal>
                    <ogc:Literal>7</ogc:Literal>
                </ogc:Function>
              </sld:CssParameter>
              <sld:CssParameter name="font-style">normal</sld:CssParameter>
              <sld:CssParameter name="font-weight">bold</sld:CssParameter>
            </sld:Font>
            <sld:LabelPlacement>
              <sld:PointPlacement>
                <sld:AnchorPoint>
                  <sld:AnchorPointX>-0.2</sld:AnchorPointX>
                  <sld:AnchorPointY>0.2</sld:AnchorPointY>
                </sld:AnchorPoint>
              </sld:PointPlacement>
            </sld:LabelPlacement>
            <sld:Halo>
              <sld:Radius>0.1</sld:Radius>
              <sld:Fill>
                <sld:CssParameter name="fill">#E5E8EC</sld:CssParameter>
              </sld:Fill>
            </sld:Halo>
            <sld:Fill>
              <sld:CssParameter name="fill">#040404</sld:CssParameter>
            </sld:Fill>
          </sld:TextSymbolizer>
        </sld:Rule>
       
      </sld:FeatureTypeStyle>
    </sld:UserStyle>
  </sld:NamedLayer>
</sld:StyledLayerDescriptor>