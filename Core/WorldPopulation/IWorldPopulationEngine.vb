
Public Interface IWorldPopulationEngine

    ReadOnly Property Population As IList(Of Person)

    Property Wallet As IDictionary(Of String, Double)

    Sub ProduceMoney()

    Sub RunPopulationCampaign(campaign As IPopulationCampaign)

    Sub RunSpendingCampaign(campaign As ISpendingCampaign, market As IMarket)
End Interface
