<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>T_GIS_Layer_KQZ2017_QV</Name>
    <UserStyle>
   
      
      <FeatureTypeStyle>
        <Rule>   
            <PointSymbolizer>
              <Geometry>
                <PropertyName xmlns="http://www.opengis.net/ogc">the_geom</PropertyName>
              </Geometry>
              <Graphic>
                <Mark>
                  <WellKnownName>CIRCLE</WellKnownName>
                  <Fill><CssParameter name="fill">#5780b9</CssParameter></Fill>
                  <Stroke><CssParameter name="stroke">#244d86</CssParameter></Stroke>
                </Mark>
                
                <Size>
                <ogc:Function name="Categorize">
                    <ogc:Function name="env">
                      <ogc:Literal>wms_scale_denominator</ogc:Literal>
                    </ogc:Function>
                    <!-- Range [<= 1000]=14, [1000-3000]=4, [3000-5000]=4, and [>5000]=5 -->
                    <ogc:Literal>8</ogc:Literal><ogc:Literal>5000</ogc:Literal>
					<ogc:Literal>8</ogc:Literal><ogc:Literal>10000</ogc:Literal>
					<ogc:Literal>9</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                    <ogc:Literal>7</ogc:Literal><ogc:Literal>35000</ogc:Literal>
                    <ogc:Literal>6</ogc:Literal><ogc:Literal>150000</ogc:Literal>
                    <ogc:Literal>3</ogc:Literal><ogc:Literal>300000</ogc:Literal>
                    <ogc:Literal>2</ogc:Literal>
                </ogc:Function>
              </Size>
                
              </Graphic>
          </PointSymbolizer>
 	</Rule>
        
	<Rule>   
		<MaxScaleDenominator>5000</MaxScaleDenominator> 
          <TextSymbolizer>
          <Label>
              <ogc:PropertyName>KODI</ogc:PropertyName>
            </Label>            
            <Font>
              <CssParameter name="font-family">Arial</CssParameter>
              <CssParameter name="font-size">12</CssParameter>
              <CssParameter name="font-style">Normal</CssParameter>
              <CssParameter name="font-weight">bold</CssParameter>
            </Font>
            <LabelPlacement>
              <PointPlacement>
                <AnchorPoint>
                  <AnchorPointX>0.4</AnchorPointX>
                  <AnchorPointY>-0.6</AnchorPointY>
                </AnchorPoint>
              </PointPlacement>
            </LabelPlacement>
            <Halo>
              <Radius>1</Radius>
              <Fill>
                <CssParameter name="fill">#ffffff</CssParameter>
              </Fill>
            </Halo>
            <Fill>
              <CssParameter name="fill">#002673</CssParameter> 
            </Fill>
          </TextSymbolizer>

        </Rule>
      </FeatureTypeStyle>
      
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>