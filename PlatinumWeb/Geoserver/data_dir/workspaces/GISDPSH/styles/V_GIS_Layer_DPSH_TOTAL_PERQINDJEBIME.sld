<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name></Name>
    <UserStyle>
      <Name>V_GIS_Layer_DPSH_TOTAL_PERQINDJEBIME</Name>
      
      <FeatureTypeStyle>        
        <Rule>       
          <Name>Asgjesuar në qark</Name>
          <ogc:Filter>
            <ogc:PropertyIsGreaterThan><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>50</ogc:Literal></ogc:PropertyIsGreaterThan>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#ff3308</CssParameter><CssParameter name="fill-opacity">0.01</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#ff3308</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
          </PolygonSymbolizer>	
        </Rule> 
        
        <Rule>   
          <Name>Totali i bimeve te asgjesuara ne shkalle vendi</Name>
          <ogc:Filter>
            <ogc:PropertyIsLessThanOrEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>50</ogc:Literal></ogc:PropertyIsLessThanOrEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill><CssParameter name="fill">#f5f5dc</CssParameter><CssParameter name="fill-opacity">0.01</CssParameter></Fill>
            <Stroke><CssParameter name="stroke">#f5f5dc</CssParameter><CssParameter name="fill-opacity">1</CssParameter></Stroke>
          </PolygonSymbolizer>	
        </Rule>  
        
        <Rule>
          <PointSymbolizer>
            <Geometry>
              <ogc:Function name="centroid">
                <ogc:PropertyName>the_geom</ogc:PropertyName>
              </ogc:Function>
           </Geometry>
           <Graphic>
              <ExternalGraphic>
                <OnlineResource
                  xlink:href="http://chart?cht=p&amp;chf=bg,s,00000000&amp;chd=t:${VLERA},${100 - VLERA}&amp;chco=0198E1,f5f5dc " />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Add>
                  <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>120</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                      <ogc:Literal>90</ogc:Literal><ogc:Literal>300000</ogc:Literal>
                      <ogc:Literal>70</ogc:Literal><ogc:Literal>1000000</ogc:Literal>
                      <ogc:Literal>35</ogc:Literal>
                  </ogc:Function>
                  <ogc:Mul>
                    <ogc:Div>
                      <ogc:PropertyName>VLERA</ogc:PropertyName>
                      <ogc:Literal>93</ogc:Literal>
                    </ogc:Div>
                    <ogc:Literal>20</ogc:Literal>
                  </ogc:Mul>
                </ogc:Add>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>    
      </FeatureTypeStyle>
      
      <FeatureTypeStyle>        
        <Rule>
          <MaxScaleDenominator>1100000</MaxScaleDenominator>
          <TextSymbolizer>
          <Geometry>
              <ogc:PropertyName>the_geom</ogc:PropertyName>
          </Geometry>
          <Label>
            <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><![CDATA[
			]]> <ogc:PropertyName>VLERA</ogc:PropertyName>%
          </Label>
          <Font>
            <CssParameter name="font-family">Arial</CssParameter>
            <CssParameter name="font-size">11</CssParameter>
            <CssParameter name="font-style">normal</CssParameter>
            <CssParameter name="font-weight">bold</CssParameter>
          </Font>
          <LabelPlacement>
            <PointPlacement>
              <AnchorPoint><AnchorPointX>0.5</AnchorPointX><AnchorPointY>0.0</AnchorPointY></AnchorPoint>
              <Displacement><DisplacementX>0</DisplacementX><DisplacementY>10</DisplacementY></Displacement>
              <Rotation><ogc:Literal>0</ogc:Literal></Rotation>
            </PointPlacement>
          </LabelPlacement>
          <Halo>
            <Radius>
              <ogc:Literal>1.0</ogc:Literal>
            </Radius>
            <Fill>
              <CssParameter name="fill">#FFFFFF</CssParameter>
            </Fill>
          </Halo>
          <Fill>
            <CssParameter name="fill">#000000</CssParameter>
          </Fill>
          <VendorOption name="conflictResolution">true</VendorOption>
          <VendorOption name="goodnessOfFit">0</VendorOption>
          <VendorOption name="autoWrap">150</VendorOption>
        </TextSymbolizer>
      </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>