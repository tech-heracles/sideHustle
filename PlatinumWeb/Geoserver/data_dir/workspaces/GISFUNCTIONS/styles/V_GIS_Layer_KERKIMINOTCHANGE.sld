<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_KERKIMINOTCHANGE</Name>
<UserStyle>
<Name>V_GIS_Layer_KERKIMINOTCHANGE</Name>	
	<FeatureTypeStyle>
    <Name>Funksione ne Editim</Name>
	
	
	 <!-- Fillo Pikat -->
	  <sld:Rule>
		<sld:Name>Rregulli1</sld:Name>
		<sld:Title>Point</sld:Title>
		<sld:Abstract>Assete Pika Abstract</sld:Abstract> 
		<ogc:Filter>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>LAYERGEOMETRY</ogc:PropertyName><ogc:Literal>POINT</ogc:Literal></ogc:PropertyIsEqualTo>
		</ogc:Filter> 
		<sld:PointSymbolizer>
		  <sld:Graphic>
			<sld:Mark>
			  <sld:WellKnownName>circle</sld:WellKnownName>                  
			  <sld:Fill><sld:CssParameter name="fill">#ff6600</sld:CssParameter><sld:CssParameter name="fill-opacity">0.9</sld:CssParameter></sld:Fill>
			  <sld:Stroke>
				<sld:CssParameter name="stroke">#972600</sld:CssParameter><sld:CssParameter name="stroke-opacity">1</sld:CssParameter>
				<sld:CssParameter name="stroke-width">1</sld:CssParameter>
			  </sld:Stroke>
			</sld:Mark>
			<sld:Opacity>1</sld:Opacity>
			<sld:Size>
			  <ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
				<ogc:Literal>22</ogc:Literal><ogc:Literal>2200</ogc:Literal>
				<ogc:Literal>18</ogc:Literal><ogc:Literal>5000</ogc:Literal>
				<ogc:Literal>12</ogc:Literal><ogc:Literal>10000</ogc:Literal>
				<ogc:Literal>8</ogc:Literal><ogc:Literal>100000</ogc:Literal>
				<ogc:Literal>4</ogc:Literal><ogc:Literal>500000</ogc:Literal>
				<ogc:Literal>2</ogc:Literal><ogc:Literal>1000000</ogc:Literal>
				<ogc:Literal>1</ogc:Literal>
			  </ogc:Function>
		    </sld:Size>
			<sld:Rotation>20</sld:Rotation>
		  </sld:Graphic>
		</sld:PointSymbolizer>
	  </sld:Rule> 
	  <!-- Fund Pikat -->
	   
	  <!-- Fillo Vijat-->
	  <sld:Rule>
		<sld:Name>Vijat</sld:Name>
		<sld:Title>Multilinestrig</sld:Title>
		<sld:Abstract>Assete Vija Abstract</sld:Abstract> 
		<ogc:Filter>
		  <ogc:PropertyIsEqualTo><ogc:PropertyName>LAYERGEOMETRY</ogc:PropertyName><ogc:Literal>MULTILINESTRING</ogc:Literal></ogc:PropertyIsEqualTo>
		</ogc:Filter> 
		<sld:LineSymbolizer>
		  <sld:Stroke>
            <sld:CssParameter name="stroke">#ff6600</sld:CssParameter>
            <sld:CssParameter name="stroke-width">
			  <ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
				<ogc:Literal>5</ogc:Literal><ogc:Literal>2200</ogc:Literal>
				<ogc:Literal>4</ogc:Literal><ogc:Literal>5000</ogc:Literal>
				<ogc:Literal>3</ogc:Literal><ogc:Literal>35000</ogc:Literal>
				<ogc:Literal>2</ogc:Literal><ogc:Literal>100000</ogc:Literal>
				<ogc:Literal>1</ogc:Literal><ogc:Literal>250000</ogc:Literal>
				<ogc:Literal>0.5</ogc:Literal>
			  </ogc:Function>
            </sld:CssParameter>
            <sld:CssParameter name="stroke-linecap">round</sld:CssParameter>
            <sld:CssParameter name="stroke-opacity">0.9</sld:CssParameter>
          </sld:Stroke>
		</sld:LineSymbolizer>
	  </sld:Rule>
	  <!-- Fund Vijat-->
	 
	  <!-- Fillo Poligonet--> 
	  <sld:Rule>
		<sld:Name>Rregulli3</sld:Name>
		<sld:Title>Poligone</sld:Title>
		<sld:Abstract>Assete Poligone Abstract</sld:Abstract>              
		<ogc:Filter>
		   <ogc:PropertyIsEqualTo><ogc:PropertyName>LAYERGEOMETRY</ogc:PropertyName><ogc:Literal>MULTIPOLYGON</ogc:Literal></ogc:PropertyIsEqualTo>
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