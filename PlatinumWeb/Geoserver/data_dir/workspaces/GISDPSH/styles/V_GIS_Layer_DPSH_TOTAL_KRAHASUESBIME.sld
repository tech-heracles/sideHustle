<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name></Name>
    <UserStyle>
      <Name>V_GIS_Layer_DPSH_TOTAL_KRAHASUESBIME</Name>
      <FeatureTypeStyle>
        <Rule>       
          <Name> Krahasueseve Mujore ne shkalle vendi </Name>
          <PolygonSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#b6b6b6</CssParameter><CssParameter name="stroke-width">5</CssParameter><CssParameter name="stroke-linejoin">round</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
          <PolygonSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#E0427F</CssParameter><CssParameter name="stroke-width">1.5</CssParameter><CssParameter name="stroke-linejoin">bevel</CssParameter>
            </Stroke>
          </PolygonSymbolizer> 
        </Rule>  
        
        <Rule>
	  	<MinScaleDenominator>300000</MinScaleDenominator>  
        <TextSymbolizer>
          <Geometry>
              <ogc:PropertyName>the_geom</ogc:PropertyName>
          </Geometry>
          <Label>
            <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName>
          </Label>
          <Font>
            <CssParameter name="font-family">Arial</CssParameter>
            <CssParameter name="font-style">normal</CssParameter>
            <CssParameter name="font-weight">bold</CssParameter>
            <CssParameter name="font-size">
               <ogc:Function name="Categorize">
                 <ogc:Function name="env">
                   <ogc:Literal>wms_scale_denominator</ogc:Literal>
                 </ogc:Function>
                 <ogc:Literal>15</ogc:Literal>
                 <ogc:Literal>500000</ogc:Literal><ogc:Literal>11</ogc:Literal>
                 <ogc:Literal>1000000</ogc:Literal><ogc:Literal>8</ogc:Literal>
                 <ogc:Literal>3000000</ogc:Literal><ogc:Literal>6</ogc:Literal>
               </ogc:Function>
           </CssParameter>
          </Font>
          <LabelPlacement>
            <PointPlacement>
              <AnchorPoint><AnchorPointX>0.5</AnchorPointX><AnchorPointY>0.0</AnchorPointY></AnchorPoint>
              <Displacement><DisplacementX>0</DisplacementX><DisplacementY>5</DisplacementY></Displacement>
              <Rotation><ogc:Literal>0</ogc:Literal></Rotation>
            </PointPlacement>
          </LabelPlacement>
          <Halo>
            <Radius>
              <ogc:Literal>1.0</ogc:Literal>
            </Radius>
            <Fill>
              <CssParameter name="fill">#eb84ac</CssParameter>
            </Fill>
          </Halo>
          <VendorOption name="conflictResolution">true</VendorOption>
          <VendorOption name="goodnessOfFit">0</VendorOption>
          <VendorOption name="autoWrap">60</VendorOption>
        </TextSymbolizer>
      </Rule>
      </FeatureTypeStyle>
      
      <FeatureTypeStyle>
        <Rule>
	  	  <MaxScaleDenominator>300000</MaxScaleDenominator>  
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource
                        xlink:href="http://chart?cht=bvg&amp;chf=bg,s,FFE1FF90&amp;chs=800x350&amp;chma=30,30,30,30&amp;chtt=Bime te asgjesuara ne shkalle vendi &amp;chts=000000,20&amp;chxt=x,y,t&amp;chxl=0:|01|02|03|04|05|06|07|08|09|10|11|12|1:|Min|Mes|Max|2:|${VL1}|${VL2}|${VL3}|${VL4}|${VL5}|${VL6}|${VL7}|${VL8}|${VL9}|${VL10}|${VL11}|${VL12}&amp;chxs=0,000000|1,000000|2,000000&amp;chd=t:${P1}|${P2}|${P3}|${P4}|${P5}|${P6}|${P7}|${P8}|${P9}|${P10}|${P11}|${P12}&amp;chco=61B329" />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>800</ogc:Literal>
                  	  <ogc:Literal>10000</ogc:Literal><ogc:Literal>800</ogc:Literal>
                  	  <ogc:Literal>50000</ogc:Literal><ogc:Literal>700</ogc:Literal>
                  	  <ogc:Literal>100000</ogc:Literal><ogc:Literal>600</ogc:Literal>
                  	  <ogc:Literal>200000</ogc:Literal><ogc:Literal>500</ogc:Literal>
                  </ogc:Function>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
        
        <Rule>
	  	<MinScaleDenominator>300000</MinScaleDenominator>  
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource
                        xlink:href="http://chart?cht=bvg&amp;chf=bg,s,FFE1FF80&amp;chs=500x300&amp;chma=0,0,0,0&amp;chd=t:${P1}|${P2}|${P3}|${P4}|${P5}|${P6}|${P7}|${P8}|${P9}|${P10}|${P11}|${P12}&amp;chco=61B329" />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>350</ogc:Literal>
                      <ogc:Literal>500000</ogc:Literal><ogc:Literal>200</ogc:Literal>
                      <ogc:Literal>1000000</ogc:Literal><ogc:Literal>100</ogc:Literal>
                  	  <ogc:Literal>3000000</ogc:Literal><ogc:Literal>10</ogc:Literal>
                  </ogc:Function>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>        
        
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>