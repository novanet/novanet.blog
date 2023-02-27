param appiId string
param location string
param functionId string
param functionDefaultHostName string

var functionApiKey = listkeys('${functionId}/host/default', '2016-08-01').functionKeys.default

// Action group which uses a web hook (the AlertHandler)
resource ag 'Microsoft.Insights/actionGroups@2022-06-01' = {
  name: 'agExceptions'
  location: 'global' // Action groups are always global
  properties: {
    enabled: true
    groupShortName: 'agExceptions'
    webhookReceivers: [
      {
        name: 'AlertHandler'
        serviceUri: 'https://${functionDefaultHostName}/api/exceptions?code=${functionApiKey}'
        useCommonAlertSchema: true
      }
    ]
  }
}

// The Alert with the KQL query, dimensions to include and frequency
resource alert 'Microsoft.Insights/scheduledQueryRules@2021-08-01' = {
  name: 'exceptions'
  location: location
  properties: {
    description: 'exceptions'
    severity: 3
    enabled: true
    scopes: [
      appiId
    ]
    targetResourceTypes: [
      'microsoft.insights/components'
    ]
    evaluationFrequency: 'PT5M'
    windowSize: 'PT5M'
    overrideQueryTimeRange: 'P2D'
    criteria: {
      allOf: [
        {
          query: 'exceptions | where timestamp > ago(5min) | where severityLevel >= 3 | project cloud_RoleName, type, outerMessage | summarize ErrorCount=count() by cloud_RoleName, type, outerMessage | project ErrorCount, cloud_RoleName, type, outerMessage | order by ErrorCount desc'
          threshold: 1
          operator: 'GreaterThanOrEqual'
          timeAggregation: 'Count'
          failingPeriods: {
            numberOfEvaluationPeriods: 1
            minFailingPeriodsToAlert: 1
          }
          dimensions: [
            {
              name: 'cloud_RoleName'
              operator: 'Include'
              values: [
                '*'
              ]
            }
            {
              name: 'type'
              operator: 'Include'
              values: [
                '*'
              ]
            }
            {
              name: 'outerMessage'
              operator: 'Include'
              values: [
                '*'
              ]
            }
            {
              name: 'ErrorCount'
              operator: 'Include'
              values: [
                '*'
              ]
            }
          ]
        }
      ]
    }
    autoMitigate: false
    actions: {
      actionGroups: [
        ag.id
      ]
    }    
  }
}
