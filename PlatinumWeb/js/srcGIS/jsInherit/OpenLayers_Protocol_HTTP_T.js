/* Copyright (c) 2006-2013 by OpenLayers Contributors (see authors.txt for
 * full list of contributors). Published under the 2-clause BSD license.
 * See license.txt in the OpenLayers distribution or repository for the
 * full text of the license. */

/**
 * @requires OpenLayers/Protocol.js
 * @requires OpenLayers/Request/XMLHttpRequest.js
 */

/**
 * if application uses the query string, for example, for BBOX parameters,
 * OpenLayers/Format/QueryStringFilter.js should be included in the build config file
 */

/**
 * Class: OpenLayers.Protocol.HTTP
 * A basic HTTP protocol for vector layers.  Create a new instance with the
 *     <OpenLayers.Protocol.HTTP> constructor.
 *
 * Inherits from:
 *  - <OpenLayers.Protocol.HTTP>
 */
//parseFeature lexon stringun qe eshte ne format geojson
OpenLayers.Protocol.HTTP_T = OpenLayers.Class(OpenLayers.Protocol.HTTP, {
    parseFeatures: function(request) {
        var doc = request.responseXML;
        if (!doc || !doc.documentElement)
        {            
            if (JSON.parse(request.responseText) && JSON.parse(request.responseText).d)
            {
                doc = JSON.parse(request.responseText).d;
            }
        }
        if (!doc || doc.length <= 0) {
            return null;
        }
        return new OpenLayers.Format.GeoJSON().read(doc);
    },
    CLASS_NAME: "OpenLayers.Protocol.HTTP_T"
});
