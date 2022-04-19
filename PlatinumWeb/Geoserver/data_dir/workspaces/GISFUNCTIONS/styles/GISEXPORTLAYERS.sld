<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_TEMPLAYERPRINDFEMI</Name>
<UserStyle>
<Name>V_GIS_Layer_TEMPLAYERPRINDFEMI</Name>	
	<FeatureTypeStyle>
    <Name>Funksione ne Editim</Name>
	
	
	 <!-- Fillo Pikat -->
	  <sld:Rule>
		<sld:Name>Rregulli1</sld:Name>
		<sld:Title>Assete Pika</sld:Title>
		<sld:Abstract>Assete Pika Abstract</sld:Abstract> 
		<ogc:Filter>
		  <ogc:Or>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>6</ogc:Literal></ogc:PropertyIsEqualTo>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>8</ogc:Literal></ogc:PropertyIsEqualTo>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>9</ogc:Literal></ogc:PropertyIsEqualTo>
		    <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>10</ogc:Literal></ogc:PropertyIsEqualTo>
		  </ogc:Or>     
		</ogc:Filter> 
		<sld:PointSymbolizer>
          <sld:Graphic>
            <sld:Mark>
              <sld:WellKnownName>circle</sld:WellKnownName>                  
              <sld:Fill><sld:CssParameter name="fill">#ff6600</sld:CssParameter><sld:CssParameter name="fill-opacity">0.9</sld:CssParameter></sld:Fill>
              <sld:Stroke>
                <sld:CssParameter name="stroke">#7b3e00</sld:CssParameter><sld:CssParameter name="stroke-opacity">1</sld:CssParameter>
                <sld:CssParameter name="stroke-width">1</sld:CssParameter>
              </sld:Stroke>
            </sld:Mark>
            <sld:Opacity>1</sld:Opacity><sld:Size>12</sld:Size><sld:Rotation>20</sld:Rotation>
          </sld:Graphic>
        </sld:PointSymbolizer>
	  </sld:Rule> 
	  <!-- Fund Pikat -->
	   
	  <!-- Fillo Vijat-->
	  <sld:Rule>
		<sld:Name>Linja</sld:Name>
		<sld:Title>Assete Vija</sld:Title>
		<sld:Abstract>Assete Vija Abstract</sld:Abstract> 
		<ogc:Filter>
		  <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>11</ogc:Literal></ogc:PropertyIsEqualTo>
		</ogc:Filter> 
		<sld:LineSymbolizer>
		  <sld:Stroke><sld:CssParameter name="stroke">#ff6600</sld:CssParameter><sld:CssParameter name="stroke-width">5</sld:CssParameter><sld:CssParameter name="stroke-linecap">round</sld:CssParameter><sld:CssParameter name="stroke-opacity">0.9</sld:CssParameter></sld:Stroke>
		</sld:LineSymbolizer>
	  </sld:Rule>
	  <!-- Fund Vijat-->
	 
	  <!-- Fillo Poligonet--> 
	  <sld:Rule>
		<sld:Name>Rregulli3</sld:Name>
		<sld:Title>Assete Poligone </sld:Title>
		<sld:Abstract>Assete Poligone Abstract</sld:Abstract>              
		<ogc:Filter>
		   <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>5</ogc:Literal></ogc:PropertyIsEqualTo>
		</ogc:Filter>  
		<sld:PolygonSymbolizer>
		  <sld:Fill><sld:CssParameter name="fill">#ff6600</sld:CssParameter><sld:CssParameter name="fill-opacity">0.9</sld:CssParameter></sld:Fill>
		  <sld:Stroke><sld:CssParameter name="stroke">#972600</sld:CssParameter><sld:CssParameter name="stroke-opacity">1</sld:CssParameter><sld:CssParameter name="stroke-width">0.5</sld:CssParameter></sld:Stroke>
		</sld:PolygonSymbolizer>              
	  </sld:Rule>         
	  <!-- Fund Poligonet--> 
	   
	</FeatureTypeStyle> 
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>