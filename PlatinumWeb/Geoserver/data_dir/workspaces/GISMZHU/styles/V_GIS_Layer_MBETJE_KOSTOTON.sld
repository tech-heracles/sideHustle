<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name></Name>
    <UserStyle>
      <Name>V_GIS_Layer_MBETJE_KOSTOTON</Name>
      <FeatureTypeStyle>
        <Rule>       
          <Name> Rezultatet per Bashki </Name>
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
	  	<MinScaleDenominator>150000</MinScaleDenominator>  
        <TextSymbolizer>
          <Geometry>
            <ogc:Function name="centroid">
              <ogc:PropertyName>the_geom</ogc:PropertyName>
            </ogc:Function>
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
	  	<MaxScaleDenominator>150000</MaxScaleDenominator>  
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource
                        xlink:href="http://chart?cht=bvg&amp;chf=bg,s,FFE1FF90&amp;chs=500x250&amp;chma=30,30,30,30&amp;chtt=Bashkia+${PERSHKRIMI}&amp;chts=000000,20&amp;chxt=x,y,t&amp;chxl=0:|Grumbullim|Transport|Mirembajtje|Personel|Amortizim|Depozitim|1:|Min|Mes|Max|2:|${VLG}+Lek|${VLT}+Lek|${VLM}+Lek|${VLP}+Lek|${VLA}+Lek|${VLD}+Lek&amp;chxs=0,000000|1,000000|2,000000&amp;chd=t:${PG}|${PT}|${PM}|${PP}|${PA}|${PD}&amp;chco=CC3333,008AB8,279B61,FFCC33,CC6699,3F5D7D" />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>800</ogc:Literal>
                  	  <ogc:Literal>10000</ogc:Literal><ogc:Literal>600</ogc:Literal>
                  	  <ogc:Literal>50000</ogc:Literal><ogc:Literal>500</ogc:Literal>
                  	  <ogc:Literal>100000</ogc:Literal><ogc:Literal>400</ogc:Literal>
                  	  <ogc:Literal>200000</ogc:Literal><ogc:Literal>200</ogc:Literal>
                  </ogc:Function>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
        
        <Rule>
	  	<MinScaleDenominator>150000</MinScaleDenominator>  
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource
                        xlink:href="http://chart?cht=bvg&amp;chf=bg,s,FFE1FF80&amp;chs=500x300&amp;chma=0,0,0,0&amp;chd=t:${PG}|${PT}|${PM}|${PP}|${PA}|${PD}&amp;chco=CC3333,008AB8,279B61,FFCC33,CC6699,3F5D7D" />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>150</ogc:Literal>
                      <ogc:Literal>500000</ogc:Literal><ogc:Literal>100</ogc:Literal>
                      <ogc:Literal>1000000</ogc:Literal><ogc:Literal>50</ogc:Literal>
                  	  <ogc:Literal>3000000</ogc:Literal><ogc:Literal>5</ogc:Literal>
                  </ogc:Function>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>