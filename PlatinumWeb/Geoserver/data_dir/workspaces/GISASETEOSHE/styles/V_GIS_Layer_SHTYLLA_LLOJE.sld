<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
<Name>V_GIS_Layer_SHTYLLA_LLOJE</Name>
<UserStyle>
<Name>V_GIS_Layer_SHTYLLA_LLOJE</Name>
<Title>V_GIS_Layer_SHTYLLA_LLOJE</Title>
<Abstract>V_GIS_Layer_SHTYLLA_LLOJE</Abstract>	
  
<FeatureTypeStyle>
    <Name>Shtyllat sipas llojit</Name>
      
        <!-- Shtylla Betoni -->
        <Rule>
          <Title> Shtylla Betoni</Title>
		  <ogc:Filter>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Betoni</ogc:Literal></ogc:PropertyIsEqualTo>
		  </ogc:Filter>   
          <MaxScaleDenominator>2300</MaxScaleDenominator>
          <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/shtylleBetoni_Eg.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
        </Rule>
  
 		 <Rule>
          <ogc:Filter>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Betoni</ogc:Literal></ogc:PropertyIsEqualTo>
		  </ogc:Filter> 
           <MinScaleDenominator>2300</MinScaleDenominator>
          <MaxScaleDenominator>9000</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <Mark>
                <WellKnownName>circle</WellKnownName>
                <Fill>
                  <CssParameter name="fill">#00ff00</CssParameter>
                </Fill>
                <Stroke>
                  <CssParameter name="stroke">#ffffff</CssParameter>
                  <CssParameter name="stroke-width">0.5</CssParameter>
                </Stroke>
              </Mark>
              <Size>6</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
        <!-- Fund Shtylla Betoni -->
  
  		<!-- Shtylla Metalike-->
        <Rule>
          <Title> Shtylla Metalike </Title>
		  <ogc:Filter>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Metalike</ogc:Literal></ogc:PropertyIsEqualTo>
		  </ogc:Filter>   
          <MaxScaleDenominator>2300</MaxScaleDenominator>
          <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/shtylleMetalike_eg.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
        </Rule> 
  		  
  		<Rule>
          <ogc:Filter>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Metalike</ogc:Literal></ogc:PropertyIsEqualTo>
		  </ogc:Filter> 
           <MinScaleDenominator>2300</MinScaleDenominator>
          <MaxScaleDenominator>9000</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <Mark>
                <WellKnownName>circle</WellKnownName>
                <Fill>
                  <CssParameter name="fill">#00ff00</CssParameter>
                </Fill>
                <Stroke>
                  <CssParameter name="stroke">#ffffff</CssParameter>
                  <CssParameter name="stroke-width">0.5</CssParameter>
                </Stroke>
              </Mark>
              <Size>6</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
        <!-- Fund Shtylla Metalike -->
		
  <!-- Gjithe rastet e papercaktuar -->
        <Rule>
          <Title>Shtylle e papërcaktuar</Title>
          <ogc:Filter><ogc:Or>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:And>
              <ogc:PropertyIsNotEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Betoni</ogc:Literal></ogc:PropertyIsNotEqualTo>
            <ogc:PropertyIsNotEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Metalike</ogc:Literal></ogc:PropertyIsNotEqualTo>
          </ogc:And></ogc:Or></ogc:Filter>    
          <MaxScaleDenominator>2300</MaxScaleDenominator>  
           <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/shtyllaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>20</Size>
        </Graphic>
      </PointSymbolizer>
        </Rule> 
  
  		<Rule>
          <ogc:Filter><ogc:Or>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:And>
            <ogc:PropertyIsNotEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Betoni</ogc:Literal></ogc:PropertyIsNotEqualTo>
            <ogc:PropertyIsNotEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>Metalike</ogc:Literal></ogc:PropertyIsNotEqualTo>
              </ogc:And>
          </ogc:Or></ogc:Filter>  
           <MinScaleDenominator>2300</MinScaleDenominator>
          <MaxScaleDenominator>9000</MaxScaleDenominator>
          <PointSymbolizer>
            <Graphic>
              <Mark>
                <WellKnownName>circle</WellKnownName>
                <Fill>
                  <CssParameter name="fill">#808080</CssParameter>
                </Fill>
                <Stroke>
                  <CssParameter name="stroke">#ffffff</CssParameter>
                  <CssParameter name="stroke-width">0.5</CssParameter>
                </Stroke>
              </Mark>
              <Size>6</Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
		 
        
       
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>