<?xml version="1.0" encoding="ISO-8859-1" ?>
<StyledLayerDescriptor version="1.0.0" xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>V_GIS_Layer_LINJE</Name>
    <UserStyle>
      <Name>V_GIS_Layer_LINJE</Name>
      <Title>V_GIS_Layer_LINJE</Title>
      <Abstract>V_GIS_Layer_LINJE</Abstract>

      <FeatureTypeStyle>
        <Name>Linje</Name>     
        
        
        <!-- Linja Ajrore -->
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.1</Title>
		  <ogc:Filter><ogc:And>
            <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo> 
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.1*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>   
          <MaxScaleDenominator>35000</MaxScaleDenominator>
    
                    
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF0000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>   
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.2</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
			<ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.2*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#008000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.3</Title>
          <ogc:Filter><ogc:And>
				 <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
			<ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.3*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#00FFFF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        		
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.4</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
			<ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.4*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#0000FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
         <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.5</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.5*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF00FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.6</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.6*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#a55200</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.7</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.7*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#5F3F7F</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.8</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.8*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#134c00</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
        <Rule>
          <Title>Linje TM ajrore     - Fideri Nr.9</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.9*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#261300</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        <Rule>
          
          <Title>Linje TM ajrore     - Fideri Nr.10</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.10*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#005f7f</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        
        <Rule>
          
          <Title>Linje TU ajrore     - Dalja Nr.1</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.1*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF0000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        
        <Rule>
          
          <Title>Linje TU ajrore     - Dalja Nr.2</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.2*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#008000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        
        <Rule>
          
          <Title>Linje TU ajrore     - Dalja Nr.3</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.3*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#00FFFF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        
        <Rule>
          
          <Title>Linje TU ajrore     - Dalja Nr.4</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.4*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#0000FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
              <CssParameter name="stroke-linejoin">round</CssParameter><CssParameter name="stroke-linecap">round</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
		<!-- Fund Linja Ajrore -->
        
         <!-- Linja kabllore -->
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.1</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.1*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>    
          <MaxScaleDenominator>35000</MaxScaleDenominator> 
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF0000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.2</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.2*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>   
          <MaxScaleDenominator>35000</MaxScaleDenominator>  
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#008000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.3</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.3*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>  
          <MaxScaleDenominator>35000</MaxScaleDenominator>   
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#00FFFF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.4</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.4*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>  
          <MaxScaleDenominator>35000</MaxScaleDenominator>   
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#0000FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.5</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.5*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>   
          <MaxScaleDenominator>35000</MaxScaleDenominator>  
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF00FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.6</Title>
		  <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.6*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter> 
          <MaxScaleDenominator>35000</MaxScaleDenominator>    
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#a55200</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.7</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.7*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#5F3F7F</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>
        
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.8</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.8*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#134c00</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        
        <Rule>
          <Title>Linje TM kabllore - Fideri Nr.9</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.9*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#261300</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>  
        <Rule>
          
          <Title>Linje TM kabllore - Fideri Nr.10</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.10*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#005f7f</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">10 4 1 4 1 4</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          
          <Title>Linje TU kabllore - Dalja Nr.1</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.1*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF0000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">20 2 5 2 5 2</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          
          <Title>Linje TU kabllore - Dalja Nr.2</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.2*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#008000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">20 2 5 2 5 2</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          
          <Title>Linje TU kabllore - Dalja Nr.3</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.3*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#00FFFF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">20 2 5 2 5 2</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <Rule>
          
          <Title>Linje TU kabllore - Dalja Nr.4</Title>
          <ogc:Filter><ogc:And>
			<ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>
            <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.4*</ogc:Literal></ogc:PropertyIsLike>
		  </ogc:And></ogc:Filter>     
          <MaxScaleDenominator>35000</MaxScaleDenominator>
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#0000FF</CssParameter><CssParameter name="stroke-width">1.0</CssParameter><CssParameter name="stroke-dasharray">20 2 5 2 5 2</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule> 
        <!-- Fund Linja Kabllore -->   
        
        <!-- Gjithe rastet e papercaktuar Kabllore -->
        <Rule>
          <Title>Linje e papërcaktuar</Title>            
          <ogc:Filter>
            <ogc:Or>
              <ogc:Not>
                <ogc:Or>
                  <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Ajrore</ogc:Literal></ogc:PropertyIsEqualTo>
                  <ogc:PropertyIsEqualTo><ogc:PropertyName>KODI</ogc:PropertyName><ogc:Literal>Kabllore</ogc:Literal></ogc:PropertyIsEqualTo>             
                </ogc:Or>
              </ogc:Not>
              <ogc:Not>
                <ogc:Or>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.1*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.2*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.3*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.4*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.5*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.6*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.7*</ogc:Literal></ogc:PropertyIsLike>   
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.8*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.9*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Fideri Nr.10*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.1*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.2*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.3*</ogc:Literal></ogc:PropertyIsLike>
                  <ogc:PropertyIsLike wildCard="*" singleChar="." escape="!"><ogc:PropertyName>PERSHKRIMI</ogc:PropertyName><ogc:Literal>Dalja Nr.4*</ogc:Literal></ogc:PropertyIsLike>
                  </ogc:Or>
              </ogc:Not>
            </ogc:Or>
          </ogc:Filter> 
          <MaxScaleDenominator>35000</MaxScaleDenominator>  
          <LineSymbolizer><Stroke>
              <CssParameter name="stroke">#FF0000</CssParameter><CssParameter name="stroke-width">1.0</CssParameter>
          </Stroke></LineSymbolizer>
        </Rule>
        
         <!-- Teksti -->
		<Rule>
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

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>