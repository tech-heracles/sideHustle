<?xml version="1.0" encoding="UTF-8"?><sld:StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" version="1.0.0">
  <sld:NamedLayer>
    <sld:Name>T_GIS_Layer_KQZ2017_ZQV</sld:Name>
    
    <sld:UserStyle>
      <sld:Name>T_GIS_Layer_KQZ2017_ZQV</sld:Name>
      
      <sld:FeatureTypeStyle>
        <sld:Name></sld:Name>
        
        <Rule>
          <sld:Name></sld:Name>
          <sld:Title></sld:Title>
          
          <sld:PolygonSymbolizer>
            <sld:Fill>
              <sld:CssParameter name="fill">#2e61a8</sld:CssParameter>
              <sld:CssParameter name="fill-opacity">0.1</sld:CssParameter>
            </sld:Fill>
            <sld:Stroke>
              <sld:CssParameter name="stroke">#002673</sld:CssParameter>
               <sld:CssParameter name="stroke-width">1</sld:CssParameter>
            </sld:Stroke>
          </sld:PolygonSymbolizer>
        </Rule>  
        
        <Rule>  
          <MaxScaleDenominator>140000</MaxScaleDenominator> 
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
					<ogc:Literal>12</ogc:Literal><ogc:Literal>10000</ogc:Literal>
					<ogc:Literal>11</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                    <ogc:Literal>10</ogc:Literal><ogc:Literal>35000</ogc:Literal>
                    <ogc:Literal>8</ogc:Literal><ogc:Literal>70000</ogc:Literal>
                    <ogc:Literal>3</ogc:Literal><ogc:Literal>140000</ogc:Literal>
                    <ogc:Literal>2</ogc:Literal>
                    </ogc:Function></sld:CssParameter>
              <sld:CssParameter name="font-style">Normal</sld:CssParameter>
              <sld:CssParameter name="font-weight">bold</sld:CssParameter>
            </sld:Font>

            <sld:LabelPlacement>
              <sld:PointPlacement>
                <sld:AnchorPoint>
                  <sld:AnchorPointX>0.5</sld:AnchorPointX>
                  <sld:AnchorPointY>0.5</sld:AnchorPointY>
                </sld:AnchorPoint>
              </sld:PointPlacement>
            </sld:LabelPlacement>
            <sld:Halo>
              <sld:Radius>1.2</sld:Radius>
              <sld:Fill>
                <sld:CssParameter name="fill">#ffffff</sld:CssParameter>
              </sld:Fill>
            </sld:Halo>
            <sld:Fill>
              <sld:CssParameter name="fill">#343434</sld:CssParameter> 
            </sld:Fill>
          </sld:TextSymbolizer>
          
        </Rule>
        
      </sld:FeatureTypeStyle>
      
    </sld:UserStyle>
  </sld:NamedLayer>
</sld:StyledLayerDescriptor>