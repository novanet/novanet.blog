param alertOriginName string
param alertHandlerName string
param location string = resourceGroup().location

// Application Insights
module appi 'modules/appi.bicep' = {
  name: 'appi'
  params: {
    azureResourceName: 'appi-alerts'
    location: location
  }
}

// Function - AlertHandler
module alertHandler 'modules/func-alerthandler.bicep' = {
 name: 'AlertHandler'
 params: {
  azureResourceName: alertHandlerName
  location: location
  appiInstrumentationKey: appi.outputs.instrumentationKey
 }
}

// App - AlertOrigin
module alertOrigin 'modules/app-alertorigin.bicep' = {
  name: 'AlertOrigin'
  params: {
   azureResourceName: alertOriginName
   location: location
   appiInstrumentationKey: appi.outputs.instrumentationKey
  }
 }

// Alert
module alert 'modules/alert-exceptions.bicep' = {
  name: 'AlertExceptions'
  params: {
    appiId: appi.outputs.id
    functionDefaultHostName: alertHandler.outputs.defaultHostName
    functionId: alertHandler.outputs.id
    location: location
  }
}
