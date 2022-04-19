<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
 <NamedLayer>
        <Name>T_GIS_Layer_NEWALBANIAMUNICIPALITIES</Name>
        <UserStyle>
            <Name>T_GIS_Layer_NEWALBANIAMUNICIPALITIES</Name>
            <FeatureTypeStyle>
                <Rule>
                    <MaxScaleDenominator>2200000</MaxScaleDenominator>
        
                    <TextSymbolizer>
                        <Label>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                        </Label>
                        <Font>
                            <CssParameter name="font-family">Arial</CssParameter>
                            <CssParameter name="font-size">12</CssParameter>
                            <CssParameter name="font-style">normal</CssParameter>
                            <CssParameter name="font-weight">bold</CssParameter>
                        </Font>
                        <LabelPlacement>
                            <PointPlacement>          
                                <Displacement>
                                    <DisplacementX>0</DisplacementX>
                                    <DisplacementY>0</DisplacementY>
                                </Displacement>                
                            </PointPlacement>
                        </LabelPlacement>
                        <Halo>
                            <Radius>1.5</Radius>
                            <Fill>
                                <CssParameter name="fill">#f1f1f1</CssParameter>
                            </Fill>
                        </Halo>
                        <Fill>
                            <CssParameter name="fill">#222222</CssParameter>
                        </Fill>
                        <VendorOption name="maxDisplacement">50</VendorOption>
                        <VendorOption name="autoWrap">100</VendorOption>
                        <VendorOption name="conflictResolution">false</VendorOption>
                        <VendorOption name="goodnessOfFit">0.1</VendorOption>            
                    </TextSymbolizer>
            
           
                </Rule>
        
                <Rule>          
              
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>Null</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
        
                <Rule>
                    <Name>BELSH</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>BELSH</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#73ffdf</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>BERAT</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>BERAT</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#a1fc95</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>BULQIZË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>BULQIZË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#92edfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>CËRRIK</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>CËRRIK</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcdab1</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DELVINË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DELVINË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fc9692</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DEVOLL</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DEVOLL</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#958dfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DIBËR</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DIBËR</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#95b9fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DIVJAKË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DIVJAKË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#cafcd0</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DROPULL</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DROPULL</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#f8fc9a</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>DURRËS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>DURRËS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#f98dfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>ELBASAN</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>ELBASAN</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffffbe</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>FIER</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>FIER</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d4d9fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>FUSHË ARRËS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>FUSHË ARRËS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#90fcc8</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>GJIROKASTËR</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>GJIROKASTËR</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d4fcf8</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>GRAMSH</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>GRAMSH</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fc90c4</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>HAS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>HAS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcb48d</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>HIMARË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>HIMARË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#b9acfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KAMËZ</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KAMËZ</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#cafca2</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KAVAJË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KAVAJË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d1ff73</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KËLCYRË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KËLCYRË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#c897fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KLOS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KLOS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fc9fb8</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KOLONJË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KOLONJË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#f8d4fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KONISPOL</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KONISPOL</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#b6e3fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KORÇË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KORÇË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#aefceb</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KRUJË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KRUJË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#73b2ff</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KUKËS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KUKËS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcdd92</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KURBIN</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KURBIN</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcc2ae</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>KUÇOVË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>KUÇOVË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#909efc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>LEZHË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>LEZHË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#a3ff73</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>LIBOHOVË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>LIBOHOVË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fce3d2</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>LIBRAZHD</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>LIBRAZHD</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#b3fcbc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>FINIQ</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>FINIQ</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#cac0fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>LUSHNJE</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>LUSHNJE</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d9acfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MALIQ</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MALIQ</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#c3fc8d</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MALLAKASTËR</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MALLAKASTËR</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffa77f</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MALËSI E MADHE</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MALËSI E MADHE</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#9afcaf</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MAT</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MAT</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffffbe</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MEMALIAJ</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MEMALIAJ</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fca9d8</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>MIRDITË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>MIRDITË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#73dfff</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>PATOS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PATOS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffbebe</CssParameter>
                        </Fill>
                    </PolygonSymbolizer>
                    <LineSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                            <CssParameter name="stroke-linecap">square</CssParameter>
                        </Stroke>
                    </LineSymbolizer>
                </Rule>
                <Rule>
                    <Name>PEQIN</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PEQIN</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d1ff73</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>POGRADEC</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>POGRADEC</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#aefcd7</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>POLIÇAN</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>POLIÇAN</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#b1f2fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>PRRENJAS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PRRENJAS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcc2e3</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>PUKË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PUKË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#dd8dfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>PUSTEC</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PUSTEC</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffffbe</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>PËRMET</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>PËRMET</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#bee8ff</CssParameter>
                        </Fill>
                    </PolygonSymbolizer>
                    <LineSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                            <CssParameter name="stroke-linecap">square</CssParameter>
                        </Stroke>
                    </LineSymbolizer>
                </Rule>
                <Rule>
                    <Name>ROSKOVEC</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>ROSKOVEC</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ddfc8d</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>RROGOZHINË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>RROGOZHINË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#95fbfc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>SARANDË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>SARANDË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ff73df</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>SELENICË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>SELENICË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#8dd0fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>SHIJAK</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>SHIJAK</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#eafca7</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>SHKODËR</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>SHKODËR</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ffffbe</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>SKRAPAR</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>SKRAPAR</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fceab3</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>TEPELENË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>TEPELENË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fcecca</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>TIRANË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>TIRANË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#ff7f7f</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>TROPOJË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>TROPOJË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#fc90a0</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>URA VAJGURORE</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>URA VAJGURORE</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#90c4fc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>VAU I DEJËS</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>VAU I DEJËS</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#92defc</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>VLORË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>VLORË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#dffcc7</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
                <Rule>
                    <Name>VORË</Name>
                    <ogc:Filter xmlns:ogc="http://www.opengis.net/ogc">
                        <ogc:PropertyIsEqualTo>
                            <ogc:PropertyName>EMRIBASHKI</ogc:PropertyName>
                            <ogc:Literal>VORË</ogc:Literal>
                        </ogc:PropertyIsEqualTo>
                    </ogc:Filter>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#73ffdf</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#f0f0f0</CssParameter>
                            <CssParameter name="stroke-width">0.4</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                </Rule>
            </FeatureTypeStyle>
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>