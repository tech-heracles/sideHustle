<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
 <NamedLayer>
        <Name>T_GIS_Layer_NEWALBANIALGUDIVISION</Name>
        <UserStyle>
            <Name>T_GIS_Layer_NEWALBANIALGUDIVISION</Name>
            <Title>T_GIS_Layer_NEWALBANIALGUDIVISION</Title>
            <Abstract>A sample style that draws a polygon</Abstract>
            <FeatureTypeStyle>
                <Name>name</Name>
                <Rule>
                    <Abstract>A polygon with a gray fill and a 1 pixel black outline</Abstract>
                    <PolygonSymbolizer>
                        <Fill>
                            <CssParameter name="fill">#d3cc40</CssParameter>
                            <CssParameter name="fill-opacity">0.01</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#d3cc40</CssParameter>
                            <CssParameter name="stroke-width">2.0</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
          
                </Rule>
                <Rule>
          
                    <MaxScaleDenominator>550000</MaxScaleDenominator>

                    <TextSymbolizer>
                        <Label>
                            <ogc:PropertyName>EMRINJAP</ogc:PropertyName>
                        </Label>
                        <Font>
                            <CssParameter name="font-family">Arial</CssParameter>
                            <CssParameter name="font-size">
                                <ogc:Function name="Categorize">
                                    <ogc:Function name="env">
                                      <ogc:Literal>wms_scale_denominator</ogc:Literal>
                                    </ogc:Function>
                                    <!-- Range [<= 550000]=6, [300000-200000]=14, [3000-5000]=8, and [>100000]=6 -->
                                    <ogc:Literal>13</ogc:Literal><ogc:Literal>300000</ogc:Literal>
                                    <ogc:Literal>9</ogc:Literal><ogc:Literal>550000</ogc:Literal>
                                    <ogc:Literal>6</ogc:Literal>
                                </ogc:Function>              
                          </CssParameter>
                            <CssParameter name="font-style">normal</CssParameter>
                            <CssParameter name="font-weight">bold</CssParameter>
                        </Font>
                        <LabelPlacement>
                            <PointPlacement>
                                <AnchorPoint>
                                    <AnchorPointX>0.0</AnchorPointX>
                                    <AnchorPointY>0.5</AnchorPointY>
                                </AnchorPoint>
                            </PointPlacement>
                        </LabelPlacement>
                        <Halo>
                            <Radius>1.3</Radius>
                            <Fill>
                                <CssParameter name="fill">#FFFFFF</CssParameter>
                            </Fill>
                        </Halo>
                        <Fill>
                            <CssParameter name="fill">#000000</CssParameter>
                        </Fill>
                    </TextSymbolizer>
                </Rule>       
            </FeatureTypeStyle>
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>