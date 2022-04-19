<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
    <NamedLayer>
        <Name>polygon</Name>
        <UserStyle>
            <FeatureTypeStyle> 
             
                <Rule>
                    <PolygonSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#b6b6b6</CssParameter>
                            <CssParameter name="stroke-width">11</CssParameter>
                            <CssParameter name="stroke-linejoin">round</CssParameter>
                            <CssParameter name="stroke-linecap">square</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>
                    <PolygonSymbolizer>
                        <Stroke>
                            <CssParameter name="stroke">#ff00ff</CssParameter>
                            <CssParameter name="stroke-width">2</CssParameter>
                            <CssParameter name="stroke-linejoin">bevel</CssParameter>
                            <CssParameter name="stroke-linecap">square</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>      
               
             
                </Rule>  
                <Rule>
                    <MinScaleDenominator>500000</MinScaleDenominator>
                    <TextSymbolizer>
                        <Label>
                            <ogc:PropertyName>EMERQARKU</ogc:PropertyName>
                        </Label>            
                        <Font>
                            <CssParameter name="font-family">Arial</CssParameter>
                            <CssParameter name="font-size">15</CssParameter>
                            <CssParameter name="font-style">Normal</CssParameter>
                            <CssParameter name="font-weight">bold</CssParameter>
                        </Font>
                        <LabelPlacement>
                            <PointPlacement>
                                <AnchorPoint>
                                    <AnchorPointX>0.5</AnchorPointX>
                                    <AnchorPointY>0.5</AnchorPointY>
                                </AnchorPoint>
                            </PointPlacement>
                        </LabelPlacement>
                        <Halo>
                            <Radius>2.5</Radius>
                            <Fill>
                                <CssParameter name="fill">#ffffff</CssParameter>
                            </Fill>
                        </Halo>
                        <Fill>
                            <CssParameter name="fill">#ff00ff</CssParameter> 
                        </Fill>
                    </TextSymbolizer>
       
                </Rule>       
            </FeatureTypeStyle>  
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>