<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>T_GIS_Layer_ROADSSTREETSCATEGORY1</Name>
<UserStyle>
<Name>T_GIS_Layer_ROADSSTREETSCATEGORY1</Name>
	
<FeatureTypeStyle>
    <Name>ROADSSTREETSCATEGORY1</Name>
	
	<Rule>
      <Name>Autostrade</Name>
      <Title>Autostrade</Title>
      <ogc:Filter>
        <ogc:PropertyIsEqualTo>
          <ogc:PropertyName>KATEGORIA</ogc:PropertyName>
          <ogc:Literal>Autostrade</ogc:Literal>
        </ogc:PropertyIsEqualTo>
      </ogc:Filter>    
      
      <LineSymbolizer>
        <Stroke>
          <CssParameter name="stroke">#f4faf6</CssParameter>
          <CssParameter name="stroke-width">12</CssParameter>
          <CssParameter name="stroke-linejoin">round</CssParameter>            
        </Stroke>
      </LineSymbolizer>	
      <LineSymbolizer>
        <Stroke>
          <CssParameter name="stroke">#f4ff13</CssParameter>
          <CssParameter name="stroke-width">10</CssParameter>              
          <CssParameter name="stroke-linejoin">round</CssParameter>
        </Stroke>
      </LineSymbolizer>
    </Rule>
  	
  	<Rule>
      <Name>Kategoria 1</Name>
      <Title>Kategoria 1</Title>
      <ogc:Filter>
        <ogc:PropertyIsEqualTo>
          <ogc:PropertyName>KATEGORIA</ogc:PropertyName>
          <ogc:Literal>Kategoria 1</ogc:Literal>
        </ogc:PropertyIsEqualTo>
      </ogc:Filter> 
      
      <LineSymbolizer>
        <Stroke>
          <CssParameter name="stroke">#616161</CssParameter>
          <CssParameter name="stroke-width">1.06</CssParameter>
          <CssParameter name="stroke-linejoin">bevel</CssParameter>
          <CssParameter name="stroke-linecap">square</CssParameter>
        </Stroke>
      </LineSymbolizer>
      <LineSymbolizer>
        <Stroke>
          <CssParameter name="stroke">#ff0000</CssParameter>
          <CssParameter name="stroke-width">1.1</CssParameter>
          <CssParameter name="stroke-linejoin">round</CssParameter>
          <CssParameter name="stroke-linecap">square</CssParameter>
        </Stroke>
      </LineSymbolizer>
    </Rule>
</FeatureTypeStyle> 
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>