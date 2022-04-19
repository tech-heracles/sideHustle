<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <!-- a Named Layer is the basic building block of an SLD document -->
   <NamedLayer>
        <Name>Rivers</Name>
        <UserStyle>
            <Name>Lakes,Lagoon</Name>
            <FeatureTypeStyle>
              	<Title>Water Surface</Title>
              
                <Rule>
                  	<Title>Lakes,Lagoon</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                      	<ogc:Or>
                        	<ogc:PropertyIsEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Lake</ogc:Literal></ogc:PropertyIsEqualTo>
                          	<ogc:PropertyIsEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Lake Island</ogc:Literal></ogc:PropertyIsEqualTo>
                    	</ogc:Or>
                    </ogc:Filter>     
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#2E78E6</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">0.26</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
              
              	<Rule>
                  	<Title>Reservoirs</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                      	<ogc:Or>
                        	<ogc:PropertyIsEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Reservoir</ogc:Literal></ogc:PropertyIsEqualTo>
                          	<ogc:PropertyIsEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Reservoir Island</ogc:Literal></ogc:PropertyIsEqualTo>
                    	</ogc:Or>
                    </ogc:Filter>     
                  	<MaxScaleDenominator>50000</MaxScaleDenominator>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#2E78E6</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">0.26</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
              
              <Rule>
                  	<Title>Other</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                      	<ogc:Or>
                          	<ogc:PropertyIsNotEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Lake</ogc:Literal></ogc:PropertyIsNotEqualTo>
                          	<ogc:PropertyIsNotEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Lake Island</ogc:Literal></ogc:PropertyIsNotEqualTo>
                        	<ogc:PropertyIsNotEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Reservoir</ogc:Literal></ogc:PropertyIsNotEqualTo>
                          	<ogc:PropertyIsNotEqualTo><ogc:PropertyName>TYPE</ogc:PropertyName><ogc:Literal>Reservoir Island</ogc:Literal></ogc:PropertyIsNotEqualTo>
                    	</ogc:Or>
                    </ogc:Filter>     
                  	<MaxScaleDenominator>50000</MaxScaleDenominator>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#2E78E6</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">0.26</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>

       
            </FeatureTypeStyle>
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>