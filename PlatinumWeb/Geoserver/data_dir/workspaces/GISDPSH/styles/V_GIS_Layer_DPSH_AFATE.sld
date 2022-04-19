<?xml version="1.0" encoding="UTF-8"?><StyledLayerDescriptor xmlns="http://www.opengis.net/sld" xmlns:sld="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:gml="http://www.opengis.net/gml" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.0.0">
<NamedLayer>
<Name>V_GIS_Layer_DPSH_AFATE1</Name>
<UserStyle>
<Name>V_GIS_Layer_DPSH_AFATE1</Name>
	
<FeatureTypeStyle>	

  	<Rule>
      <Name></Name><Title>Monitorim-Fund Afati</Title>
      <ogc:Filter><ogc:And>
        <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>18</ogc:Literal></ogc:PropertyIsEqualTo>
        <ogc:PropertyIsGreaterThanOrEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsGreaterThanOrEqualTo>
 	  </ogc:And></ogc:Filter> 
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/sirenaMB.png"/><Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>
			<ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
                <ogc:Literal>43</ogc:Literal><ogc:Literal>10000</ogc:Literal>
                <ogc:Literal>38</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                <ogc:Literal>33</ogc:Literal><ogc:Literal>30000</ogc:Literal>
                <ogc:Literal>28</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                <ogc:Literal>23</ogc:Literal><ogc:Literal>280000</ogc:Literal>
                <ogc:Literal>18</ogc:Literal>
			</ogc:Function>
		  </Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>  
	
  <Rule>
      <Name></Name><Title>Shkresa-Fund Afati</Title>
      <ogc:Filter><ogc:And>
        <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>19</ogc:Literal></ogc:PropertyIsEqualTo>
        <ogc:PropertyIsGreaterThanOrEqualTo><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsGreaterThanOrEqualTo>
 	  </ogc:And></ogc:Filter> 
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/sirenaIB.png"/><Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>
			<ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
                <ogc:Literal>43</ogc:Literal><ogc:Literal>10000</ogc:Literal>
                <ogc:Literal>38</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                <ogc:Literal>33</ogc:Literal><ogc:Literal>30000</ogc:Literal>
                <ogc:Literal>28</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                <ogc:Literal>23</ogc:Literal><ogc:Literal>280000</ogc:Literal>
                <ogc:Literal>18</ogc:Literal>
			</ogc:Function>
		  </Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>  
  
    <Rule>
      <Name></Name><Title>Monitorim-Jashte Afati</Title>
      <ogc:Filter><ogc:And>
        <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>18</ogc:Literal></ogc:PropertyIsEqualTo>
        <ogc:PropertyIsLessThan><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsLessThan>
 	  </ogc:And></ogc:Filter> 
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/sirenaMR.png"/><Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>
			<ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
                <ogc:Literal>43</ogc:Literal><ogc:Literal>10000</ogc:Literal>
                <ogc:Literal>38</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                <ogc:Literal>33</ogc:Literal><ogc:Literal>30000</ogc:Literal>
                <ogc:Literal>28</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                <ogc:Literal>23</ogc:Literal><ogc:Literal>280000</ogc:Literal>
                <ogc:Literal>18</ogc:Literal>
			</ogc:Function>
		  </Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>  
  
	<Rule>
      <Name></Name><Title>Shkresa-Jashte Afati</Title>
      <ogc:Filter><ogc:And>
        <ogc:PropertyIsEqualTo><ogc:PropertyName>LLOJI</ogc:PropertyName><ogc:Literal>19</ogc:Literal></ogc:PropertyIsEqualTo>
        <ogc:PropertyIsLessThan><ogc:PropertyName>VLERA</ogc:PropertyName><ogc:Literal>0</ogc:Literal></ogc:PropertyIsLessThan>
 	  </ogc:And></ogc:Filter> 
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/sirenaIR.png"/><Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>
			<ogc:Function name="Categorize">
				<ogc:Function name="env">
				  <ogc:Literal>wms_scale_denominator</ogc:Literal>
				</ogc:Function>
                <ogc:Literal>43</ogc:Literal><ogc:Literal>10000</ogc:Literal>
                <ogc:Literal>38</ogc:Literal><ogc:Literal>20000</ogc:Literal>
                <ogc:Literal>33</ogc:Literal><ogc:Literal>30000</ogc:Literal>
                <ogc:Literal>28</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                <ogc:Literal>23</ogc:Literal><ogc:Literal>280000</ogc:Literal>
                <ogc:Literal>18</ogc:Literal>
			</ogc:Function>
		  </Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>  
  
  	<Rule>
      <MaxScaleDenominator>250000</MaxScaleDenominator>
      <TextSymbolizer>
        <Geometry>
          <ogc:Function name="centroid">
            <ogc:PropertyName>the_geom</ogc:PropertyName>
          </ogc:Function>
        </Geometry>
        <Label>
          <ogc:PropertyName>VLERA</ogc:PropertyName>
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
			   <ogc:Literal>15</ogc:Literal><ogc:Literal>1000</ogc:Literal>
			   <ogc:Literal>13</ogc:Literal><ogc:Literal>2000</ogc:Literal>
			   <ogc:Literal>11</ogc:Literal><ogc:Literal>50000</ogc:Literal>
			   <ogc:Literal>9</ogc:Literal><ogc:Literal>100000</ogc:Literal>
			   <ogc:Literal>8</ogc:Literal>
			 </ogc:Function>
         </CssParameter>
        </Font>
        <LabelPlacement>
          <PointPlacement>
            <AnchorPoint><AnchorPointX>0.5</AnchorPointX><AnchorPointY>0.0</AnchorPointY></AnchorPoint>
            <Displacement><DisplacementX>0</DisplacementX>
              <DisplacementY>
              <ogc:Function name="Categorize">
			   <ogc:Function name="env">
				 <ogc:Literal>wms_scale_denominator</ogc:Literal>
			   </ogc:Function>
			   <ogc:Literal>25</ogc:Literal><ogc:Literal>1000</ogc:Literal>
                <ogc:Literal>20</ogc:Literal><ogc:Literal>10000</ogc:Literal>
			   <ogc:Literal>11</ogc:Literal><ogc:Literal>60000</ogc:Literal>
                <ogc:Literal>10</ogc:Literal><ogc:Literal>100000</ogc:Literal>
			   <ogc:Literal>5</ogc:Literal>
			 </ogc:Function>
              </DisplacementY>
            </Displacement>
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
  
</FeatureTypeStyle> 
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>