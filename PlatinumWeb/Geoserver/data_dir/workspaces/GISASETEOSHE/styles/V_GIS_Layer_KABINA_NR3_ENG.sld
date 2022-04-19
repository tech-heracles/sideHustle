<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
<Name>V_GIS_Layer_KABINA_NR3_ENG</Name>
<UserStyle>
<Name>V_GIS_Layer_KABINA_NR3_ENG</Name>
  <Title>V_GIS_Layer_KABINA_NR3_ENG</Title>
      <Abstract>V_GIS_Layer_KABINA_NR3_ENG</Abstract>
  <FeatureTypeStyle>
    <Name>Kabina</Name>
	
 <!-- Kabina Shtyllore e re betoni -->
    		  
  <Rule>
      	<Name>Concrete Cabin Pole</Name>
      	<Title>Concrete Cabin Pole</Title>
    	<ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHB</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere1.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>	  
  <Rule>
      	<Name>Concrete Cabin Pole</Name>
      	<Title>Concrete Cabin Pole</Title> 
    	<ogc:Filter><ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHB</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
      	<MinScaleDenominator>1101</MinScaleDenominator>
    	<MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere1.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
    
   <Rule>
      <Name>Concrete Cabin Pole</Name>
      <Title>Concrete Cabin Pole</Title>
     <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHB</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere1.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
    
    <Rule>
      <Name>Concrete Cabin Pole</Name>
      <Title>Concrete Cabin Pole</Title> 
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHB</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere1.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Name>Concrete Cabin Pole</Name>
      <Title>Concrete Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHB</ogc:Literal></ogc:PropertyIsEqualTo></ogc:Filter> 
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere1.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	


    <!-- Kabina Shtyllore e re metalike -->
    <Rule>
      <Name>Metal Cabin Pole</Name>
      <Title>Metal Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHM</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>       
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>
    
  <Rule>
      <Name>Metal Cabin Pole</Name>
      <Title>Metal Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHM</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>       
      <MinScaleDenominator>1101</MinScaleDenominator>
    <MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
		
   <Rule>
      <Name>Metal Cabin Pole</Name>
      <Title>Metal Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHM</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>       
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
    
     <Rule>
      <Name>Metal Cabin Pole</Name>
      <Title>Metal Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHM</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>   
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Name>Metal Cabin Pole</Name>
      <Title>Metal Cabin Pole</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>KSHM</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>   
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaSHTYLLOREere2.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	
    
  
	<!-- Fund Kabina Shtyllore e re -->
  
    
  		<!-- Kabina Box e re -->
   		<Rule>
      <Name>Box Cabin</Name>
      <Title>Box Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Box</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Name>Box Cabin</Name>
      <Title>Box Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Box</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>  
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	
		
   <Rule>
      <Name>Box Cabin</Name>
      <Title>Box Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Box</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Box Cabin</Name>
      <Title>Box Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Box</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
      <MinScaleDenominator>1100</MinScaleDenominator>
    <MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Box Cabin</Name>
      <Title>Box Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Box</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>     
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaBOXere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule> 
        <!-- Fund Kabina Box e re -->
		
 		<!-- Kabina Murature e re -->
      <Rule>
      <Name>Mansonry Cabin</Name>
      <Title>Mansonry Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter> 
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Name>Mansonry Cabin</Name>
      <Title>Mansonry Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>  
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	
		
   <Rule>
      <Name>Mansonry Cabin</Name>
      <Title>Mansonry Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Mansonry Cabin</Name>
      <Title>Mansonry Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
      <MinScaleDenominator>1100</MinScaleDenominator>
    <MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Name>Mansonry Cabin</Name>
      <Title>Mansonry Cabin</Title>
      <ogc:Filter>        
      <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Murature</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaMURATUREere.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
    </Rule>
        <!-- Fund Kabina Murature e re -->
  
    <!-- Gjithe rastet e papercaktuar -->
        
    	<Rule>
      <Title>Undefined Cabin</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>  
	  <MinScaleDenominator>5001</MinScaleDenominator>
	  <MaxScaleDenominator>10000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>12</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
	
	<Rule>
      <Title>Undefined Cabin</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>   
	  <MinScaleDenominator>10001</MinScaleDenominator>
	  <MaxScaleDenominator>18000</MaxScaleDenominator>        
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>10</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>	
		
   <Rule>
      <Title>Undefined Cabin</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
     <MinScaleDenominator>2201</MinScaleDenominator> 
     <MaxScaleDenominator>5000</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>18</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Title>Undefined Cabin</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>      
      <MinScaleDenominator>1100</MinScaleDenominator>
    <MaxScaleDenominator>2200</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>22</Size>
        </Graphic>
      </PointSymbolizer>
	</Rule>
  
  <Rule>
      <Title>Undefined Cabin</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>PAPERCAKTUAR</ogc:Literal></ogc:PropertyIsEqualTo>
          </ogc:Filter>       
      <MaxScaleDenominator>1100</MaxScaleDenominator>
      <PointSymbolizer>
        <Graphic>
          <ExternalGraphic>
            <OnlineResource xlink:type="simple" xlink:href="LegendIMG/kabinaPapercaktuar.png"/>
            <Format>image/PNG</Format>
          </ExternalGraphic>
          <Size>25</Size>
        </Graphic>
      </PointSymbolizer>
  </Rule>
    
       
        <!-- Teksti -->
		<Rule>
         <MaxScaleDenominator>9000</MaxScaleDenominator>
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
             <PointPlacement>
              <AnchorPoint>
                 <AnchorPointX>
                   0.5
             	 </AnchorPointX>
               	 <AnchorPointY>
                   -1.5
              	 </AnchorPointY>
              </AnchorPoint> 
            </PointPlacement>
           </LabelPlacement>
                      
           <Halo>
            <Radius>
              <ogc:Literal>1.5</ogc:Literal>
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

      </FeatureTypeStyle>
</UserStyle>
</NamedLayer>
</StyledLayerDescriptor>