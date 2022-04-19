<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <!-- a Named Layer is the basic building block of an SLD document -->
    <NamedLayer>
        <Name>RRJETI_HIDROGRAFIK</Name>
        <UserStyle>
            <Title>T_GIS_Layer_HIDROGRAPHYARC</Title>
            <Abstract>A sample style that draws a line</Abstract>
            <FeatureTypeStyle>
              	<Name>RRJETI HIDROGRAFIK</Name>
              	<Title>Hydrographic Network</Title>
                <Rule>                    
                    <Title>Rivers</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                        <ogc:PropertyIsEqualTo><ogc:PropertyName>ABBREV</ogc:PropertyName><ogc:Literal>l.</ogc:Literal></ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <LineSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">1.5</CssParameter>
                        </Stroke>
                    </LineSymbolizer>
                </Rule>
				
              	<Rule>
                    <Title>Streams</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                        <ogc:PropertyIsEqualTo><ogc:PropertyName>ABBREV</ogc:PropertyName><ogc:Literal>prr.</ogc:Literal></ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                  	<MaxScaleDenominator>70000</MaxScaleDenominator>
                    <LineSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">0.7</CssParameter>
                        </Stroke>
                    </LineSymbolizer>
                </Rule>
              
              	<Rule>
                    <Title>Channels</Title>
                    <Abstract>A solid blue line with a 1 pixel width</Abstract>
                  	<ogc:Filter>
                        <ogc:PropertyIsEqualTo><ogc:PropertyName>ABBREV</ogc:PropertyName><ogc:Literal>kan.</ogc:Literal></ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                  	<MaxScaleDenominator>50000</MaxScaleDenominator>
                    <LineSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#2E78E6</CssParameter>
                            <CssParameter name="stroke-width">0.5</CssParameter>
                        </Stroke>
                    </LineSymbolizer>
                </Rule>
              
              
            </FeatureTypeStyle>
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>