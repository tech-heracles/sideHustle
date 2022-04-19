<?xml version="1.0" encoding="UTF-8"?><sld:StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" version="1.0.0">
  <sld:NamedLayer>
    <sld:Name>V_GIS_Layer_TEMPLAYERFEMIPRIND</sld:Name>
    <sld:UserStyle>
        <sld:Name>V_GIS_Layer_TEMPLAYERFEMIPRIND</sld:Name>
        <sld:Title>Default GEOMETRY</sld:Title>
        <sld:Abstract>A sample style that draws a all Geometry</sld:Abstract>
        
         <sld:FeatureTypeStyle>
           <sld:Name>Nga femija te prindi</sld:Name>
           
           <!-- Fillo Pikat -->
           <sld:Rule>
                 <sld:Name>Rregulli1</sld:Name>
                 <sld:Title>Assete Pika</sld:Title>
                 <sld:Abstract>Assete Pika Abstract</sld:Abstract> 
                <ogc:Filter>
                   <ogc:Or>
                     <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>6</ogc:Literal></ogc:PropertyIsEqualTo>
                     <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>8</ogc:Literal></ogc:PropertyIsEqualTo>
                     <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>9</ogc:Literal></ogc:PropertyIsEqualTo>
                     <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>10</ogc:Literal></ogc:PropertyIsEqualTo>
                   </ogc:Or>     
                </ogc:Filter> 
                 <sld:PointSymbolizer>
                   <sld:Graphic>
                      <sld:Mark>
                        <sld:WellKnownName>circle</sld:WellKnownName>                  
                        <sld:Fill><sld:CssParameter name="fill">#ff6600</sld:CssParameter><sld:CssParameter name="fill-opacity">0.9</sld:CssParameter></sld:Fill>
                        <sld:Stroke>
                          <sld:CssParameter name="stroke">#7b3e00</sld:CssParameter><sld:CssParameter name="stroke-opacity">1</sld:CssParameter>
                          <sld:CssParameter name="stroke-width">1</sld:CssParameter>
                        </sld:Stroke>
                      </sld:Mark>
                      <sld:Opacity>1</sld:Opacity><sld:Size>12</sld:Size><sld:Rotation>20</sld:Rotation>
                   </sld:Graphic>
                 </sld:PointSymbolizer>
           </sld:Rule>        

          <!-- Fund Pikat -->
           
           <!-- Fillo Vijat-->
          <sld:Rule>
                 <sld:Name>Linja</sld:Name>
                 <sld:Title>Assete Vija</sld:Title>
                 <sld:Abstract>Assete Vija Abstract</sld:Abstract> 
                 <ogc:Filter>
                   <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>11</ogc:Literal></ogc:PropertyIsEqualTo>
                </ogc:Filter> 
                 <sld:LineSymbolizer>
                     <sld:Stroke><sld:CssParameter name="stroke">#ff6600</sld:CssParameter><sld:CssParameter name="stroke-width">6</sld:CssParameter><sld:CssParameter name="stroke-linecap">round</sld:CssParameter><sld:CssParameter name="stroke-opacity">0.9</sld:CssParameter></sld:Stroke>
                 </sld:LineSymbolizer>
          </sld:Rule>
          <!-- Fund Vijat-->
         
          <!-- Fillo Poligonet--> 
          <sld:Rule>
                 <sld:Name>Rregulli3</sld:Name>
                 <sld:Title>Assete Poligone </sld:Title>
                 <sld:Abstract>Assete Poligone Abstract</sld:Abstract>              
                 <ogc:Filter>
                       <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>5</ogc:Literal></ogc:PropertyIsEqualTo>
                 </ogc:Filter>  
                 <sld:PolygonSymbolizer>
                   <sld:Fill><sld:CssParameter name="fill">#ff6600</sld:CssParameter><sld:CssParameter name="fill-opacity">0.1</sld:CssParameter></sld:Fill>
                   <sld:Stroke><sld:CssParameter name="stroke">#7b3e00</sld:CssParameter><sld:CssParameter name="stroke-opacity">1</sld:CssParameter><sld:CssParameter name="stroke-width">0.5</sld:CssParameter></sld:Stroke>
                 </sld:PolygonSymbolizer>              
           </sld:Rule>         
           
            <!-- Fund Poligonet--> 
           
            <!-- Teksti Linjat -->
		<Rule>
         <ogc:Filter>
                   <ogc:PropertyIsEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>11</ogc:Literal></ogc:PropertyIsEqualTo>
                </ogc:Filter> 
          <MaxScaleDenominator>35000</MaxScaleDenominator>
         <TextSymbolizer>
           <Label>
             <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName>
           </Label>
           <Font>
             <CssParameter name="font-family">Arial</CssParameter>
             <CssParameter name="font-size">10</CssParameter>
             <CssParameter name="font-style">normal</CssParameter>
             <CssParameter name="font-weight">bold</CssParameter>
           </Font>
           <LabelPlacement>
             <LinePlacement>
              <PerpendicularOffset>13</PerpendicularOffset>
            </LinePlacement>
           </LabelPlacement>
           <Halo>
            <Radius>
              <ogc:Literal>1.0</ogc:Literal>
            </Radius>
            <Fill>
              <CssParameter name="fill">#FFFFFF</CssParameter>
            </Fill>
          </Halo>

           <VendorOption name="followLine">true</VendorOption>
           <VendorOption name="repeat">400</VendorOption>
           <VendorOption name="spaceAround">10</VendorOption>
           <VendorOption name="maxAngleDelta">15</VendorOption>
           <VendorOption name="conflictResolution">true</VendorOption>
           <VendorOption name="forceLeftToRight">false</VendorOption>
         </TextSymbolizer>
       </Rule>
           
       <!-- Teksti per elementet e tjere -->
		<Rule>
          <ogc:Filter>
      		<ogc:PropertyIsNotEqualTo><ogc:PropertyName>IDLAYERSTYPE</ogc:PropertyName><ogc:Literal>11</ogc:Literal></ogc:PropertyIsNotEqualTo>
                 </ogc:Filter>  
	  <MaxScaleDenominator>35000</MaxScaleDenominator> 
      <TextSymbolizer>
        <Geometry>
            <ogc:PropertyName>the_geom</ogc:PropertyName>
        </Geometry>
        <Label>
          <ogc:PropertyName>PERSHKRIMI</ogc:PropertyName>
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
            <Displacement><DisplacementX>0</DisplacementX><DisplacementY>5</DisplacementY></Displacement>
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
        <VendorOption name="conflictResolution">true</VendorOption>
        <VendorOption name="goodnessOfFit">0</VendorOption>
        <VendorOption name="autoWrap">60</VendorOption>
      </TextSymbolizer>
    </Rule>
           
      </sld:FeatureTypeStyle> 
      
    </sld:UserStyle>
  </sld:NamedLayer>
</sld:StyledLayerDescriptor>