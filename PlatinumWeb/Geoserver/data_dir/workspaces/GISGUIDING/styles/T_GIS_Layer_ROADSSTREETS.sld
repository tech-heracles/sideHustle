<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
<NamedLayer>
	<Name>GCC RoadsStreets</Name>
	<UserStyle>
		<Name>GCC RoadsStreets</Name>
		<Title>GCC RoadsStreets</Title>
		<Abstract> Rruget view</Abstract>

     	<FeatureTypeStyle>
      		<Name>RoadsStreets</Name>
          
			<Rule>
           		<Name>SHTETËRORE</Name>
				<Title>Shtetërore</Title>
                <Abstract>ROAD_CTGY=SHTETËRORE</Abstract>
                <ogc:Filter>
                	<ogc:PropertyIsEqualTo>
                      <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName>
                      <ogc:Literal>SHTETËRORE</ogc:Literal>
                  	</ogc:PropertyIsEqualTo>
               	</ogc:Filter>
                <LineSymbolizer>
                	<Stroke>
                    	<CssParameter name="stroke">#f4ff13</CssParameter>
                        <CssParameter name="stroke-width">3</CssParameter>
                        <CssParameter name="stroke-linejoin">round</CssParameter>
                        <CssParameter name="stroke-linecap">round</CssParameter>
                   	</Stroke>
              	</LineSymbolizer>
        	</Rule>


			<Rule>
            	<Name>LOKALE</Name>
                <Title>Lokale</Title>
                <Abstract>ROAD_CTGY=LOKALE</Abstract>
                <ogc:Filter>
                	<ogc:PropertyIsEqualTo>
                      <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName>
                      <ogc:Literal>LOKALE</ogc:Literal>
                	</ogc:PropertyIsEqualTo>
                </ogc:Filter>
               	<MaxScaleDenominator>500000</MaxScaleDenominator>
                <LineSymbolizer>
                	<Stroke>
                    	<CssParameter name="stroke">#40e0d0</CssParameter>
                        <CssParameter name="stroke-width">2</CssParameter>
                        <CssParameter name="stroke-linejoin">round</CssParameter>
                        <CssParameter name="stroke-linecap">round</CssParameter>
                        <CssParameter name="fill-opacity">0.7</CssParameter>
                  	</Stroke>
               	</LineSymbolizer>
        	</Rule>
              

          <Rule>
            <Name>KATEGORIA 1</Name>
            <Title>Kategoria 1</Title>
            <ogc:Filter>
              <ogc:And>
                <ogc:PropertyIsEqualTo>
                  <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 1</ogc:Literal>
                </ogc:PropertyIsEqualTo>
                <ogc:PropertyIsNotEqualTo>
                  <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>SHTETËRORE</ogc:Literal>
                </ogc:PropertyIsNotEqualTo>
                <ogc:PropertyIsNotEqualTo>
                  <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>LOKALE</ogc:Literal>
                </ogc:PropertyIsNotEqualTo>
                <ogc:Or>
                  <ogc:PropertyIsEqualTo>
                    <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 1</ogc:Literal>
                  </ogc:PropertyIsEqualTo>
                  <ogc:PropertyIsNull><ogc:PropertyName>ROAD_CTGY</ogc:PropertyName></ogc:PropertyIsNull>
                </ogc:Or>
              </ogc:And>
            </ogc:Filter>
            <MaxScaleDenominator>70000</MaxScaleDenominator>
            <LineSymbolizer>
              <Stroke>
                <CssParameter name="stroke">#ff0000</CssParameter>
                <CssParameter name="stroke-width">1.1</CssParameter>
                <CssParameter name="stroke-linejoin">round</CssParameter>
                <CssParameter name="stroke-linecap">square</CssParameter>
              </Stroke>
            </LineSymbolizer>
		</Rule>

		<Rule>
          <Name>Kategoria 2</Name>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 2</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>SHTETËRORE</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>LOKALE</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>
              <ogc:Or>
                <ogc:PropertyIsEqualTo>
                  <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 2</ogc:Literal>
                </ogc:PropertyIsEqualTo>
                <ogc:PropertyIsNull><ogc:PropertyName>ROAD_CTGY</ogc:PropertyName></ogc:PropertyIsNull>
              </ogc:Or>
            </ogc:And>
          </ogc:Filter>
          <MaxScaleDenominator>50000</MaxScaleDenominator>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#ffb534</CssParameter>
              <CssParameter name="stroke-width">0.7</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter>
              <CssParameter name="stroke-linecap">square</CssParameter>
            </Stroke>
          </LineSymbolizer>
		</Rule>


          <Rule>
            <Name>Kategoria 3</Name>
            <ogc:Filter>
              <ogc:And>
                <ogc:PropertyIsEqualTo>
                  <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 3</ogc:Literal
                  ></ogc:PropertyIsEqualTo>
                <ogc:PropertyIsNotEqualTo>
                  <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>SHTETËRORE</ogc:Literal>
                </ogc:PropertyIsNotEqualTo>
                <ogc:PropertyIsNotEqualTo>
                  <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>LOKALE</ogc:Literal>
                </ogc:PropertyIsNotEqualTo>
                <ogc:Or>
                  <ogc:PropertyIsEqualTo>
                    <ogc:PropertyName>STREET_CTG</ogc:PropertyName><ogc:Literal>Kategoria 3</ogc:Literal>
                  </ogc:PropertyIsEqualTo>
                  <ogc:PropertyIsNull><ogc:PropertyName>ROAD_CTGY</ogc:PropertyName></ogc:PropertyIsNull>
                </ogc:Or>
              </ogc:And>
            </ogc:Filter>
            <MaxScaleDenominator>50000</MaxScaleDenominator>
            <LineSymbolizer>
              <Stroke>
                <CssParameter name="stroke">#ffd057</CssParameter>
                <CssParameter name="stroke-width">0.7</CssParameter>
                <CssParameter name="stroke-linejoin">round</CssParameter>
                <CssParameter name="stroke-linecap">square</CssParameter>
              </Stroke>
            </LineSymbolizer>
          </Rule>


          <Rule>
            <Name>Tjetër</Name>
            <ogc:Filter>
              <ogc:And>
                <ogc:PropertyIsNull>
                  <ogc:PropertyName>STREET_CTG</ogc:PropertyName>
                </ogc:PropertyIsNull>
                <ogc:Or>
                  <ogc:PropertyIsEqualTo>
                    <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>TJETËR</ogc:Literal>
                  </ogc:PropertyIsEqualTo>
                  <ogc:PropertyIsEqualTo>
                    <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName><ogc:Literal>TJETËR2</ogc:Literal>
                  </ogc:PropertyIsEqualTo>
                  <ogc:PropertyIsNull>
                    <ogc:PropertyName>ROAD_CTGY</ogc:PropertyName>
                  </ogc:PropertyIsNull>
                </ogc:Or>
              </ogc:And>
            </ogc:Filter>
            <MaxScaleDenominator>50000</MaxScaleDenominator>
            <LineSymbolizer>
              <Stroke>
                <CssParameter name="stroke">#a61f82</CssParameter>
                <CssParameter name="stroke-width">0.7</CssParameter>
                <CssParameter name="stroke-linejoin">round</CssParameter>
                <CssParameter name="stroke-linecap">square</CssParameter>
              </Stroke>
            </LineSymbolizer>
		</Rule>

	</FeatureTypeStyle>
	</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>