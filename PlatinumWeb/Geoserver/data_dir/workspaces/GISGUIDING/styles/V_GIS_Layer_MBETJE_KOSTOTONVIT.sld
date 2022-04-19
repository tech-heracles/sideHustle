<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
  xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name></Name>
    <UserStyle>
      <Name>V_GIS_Layer_MBETJE_KOSTOTONVIT</Name>
      <FeatureTypeStyle>
        <Rule>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#dddddd</CssParameter>
            </Fill>
            <Stroke />
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
      <FeatureTypeStyle>
        <Rule>
          <PointSymbolizer>
            <Graphic>
              <ExternalGraphic>
                <OnlineResource
                        xlink:href="http://chart?chxt=x,y&amp;chxl=0:|G|T&amp;cht=bvg&amp;chco=0000ff,ff0000&amp;chf=bg,s,FFFFFF00&amp;chd=t:${TVGRUMBULLIM}|${TVTRANSPORT}&amp;chtt=T+D" />
                <Format>application/chart</Format>
              </ExternalGraphic>
              <Size>
                <ogc:Function name="Categorize">
                      <ogc:Function name="env">
                        <ogc:Literal>wms_scale_denominator</ogc:Literal>
                      </ogc:Function>
                      <ogc:Literal>200</ogc:Literal><ogc:Literal>69000</ogc:Literal>
                      <ogc:Literal>150</ogc:Literal><ogc:Literal>300000</ogc:Literal>
                      <ogc:Literal>80</ogc:Literal><ogc:Literal>1000000</ogc:Literal>
                      <ogc:Literal>60</ogc:Literal>
                  </ogc:Function>
              </Size>
            </Graphic>
          </PointSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>