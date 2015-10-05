Imports System.Collections.Generic

Public Class WorldPopulationEngine
    Implements IWorldPopulationEngine

    Private _population As New List(Of IResourceGenerator)
    Public ReadOnly Property Population As IList(Of IResourceGenerator) Implements IWorldPopulationEngine.Population
        Get
            Return _population
        End Get
    End Property

    Public Property Wallet As IDictionary(Of String, Double) = New Dictionary(Of String, Double) Implements IWorldPopulationEngine.Wallet


    Public Sub ProduceMoney() Implements IWorldPopulationEngine.ProduceMoney


        For Each subject As Person In Population.OfType(Of Person)()
            Dim moneyProduced As Double = subject.SpendMoney()
            For Each tag As String In subject.Tags
                If Wallet.ContainsKey(tag) Then
                    Wallet(tag) += moneyProduced
                Else
                    Wallet.Add(tag, moneyProduced)
                End If
            Next


        Next
    End Sub

    Public Sub RunPopulationCampaign(campaign As IPopulationCampaign) Implements IWorldPopulationEngine.RunPopulationCampaign
        If campaign Is Nothing Then Throw New ArgumentNullException("campaign", "Parameter 'campaign' cannot be nothing.")

        campaign.Run(population)

    End Sub

    Public Sub RunSpendingCampaign(campaign As ISpendingCampaign, market As IMarket) Implements IWorldPopulationEngine.RunSpendingCampaign
        If campaign Is Nothing Then Throw New ArgumentNullException("campaign", "Parameter 'campaign' cannot be nothing.")
        If market Is Nothing Then Throw New ArgumentNullException("market", "Parameter 'market' cannot be nothing.")

        campaign.RunCamapign(Wallet, market)
    End Sub


End Class




