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
                        <Fill>
                            <CssParameter name="fill">#FFFFFF</CssParameter>
                            <CssParameter name="fill-opacity">0.55</CssParameter>
                        </Fill>
                        <Stroke>
                            <CssParameter name="stroke">#FFFFFF</CssParameter>
                        </Stroke>
                    </PolygonSymbolizer>   
               
             
                </Rule>   
            </FeatureTypeStyle>  
        </UserStyle>
    </NamedLayer>
</StyledLayerDescriptor>