Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class WorldPopulationEngineUnitTests

    <TestMethod()>
    Public Sub WorldPopulationEngine_ProduceMoney()

        Dim testingEngine As New WorldPopulationEngine()
        GeneratePopulation(testingEngine.Population)

        testingEngine.ProduceMoney()

        'Verify
        Dim wallet As IDictionary(Of String, Double) = testingEngine.Wallet
        Assert.AreEqual(5.0, wallet("Poor"))
        Assert.AreEqual(30.0, wallet("Middle"))
        Assert.AreEqual(100.0, wallet("Rich"))

    End Sub

    <TestMethod()>
    Public Sub WorldPopulationEngine_SpendCampaign()
        Dim testEngine As New WorldPopulationEngine()

        Dim mockSpendingCampaign As ISpendingCampaign = Substitute.For(Of ISpendingCampaign)()

        testEngine.RunSpendingCampaign(mockSpendingCampaign, Substitute.For(Of IMarket))

        mockSpendingCampaign.ReceivedWithAnyArgs().RunCamapign(Nothing, Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub WorldPopulationEngine_SpendCampaign_NullCampaign()
        Dim testEngine As New WorldPopulationEngine()

        testEngine.RunSpendingCampaign(Nothing, Substitute.For(Of IMarket))

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub WorldPopulationEngine_SpendCampaign_NullMarket()
        Dim testEngine As New WorldPopulationEngine()

        testEngine.RunSpendingCampaign(Substitute.For(Of ISpendingCampaign), Nothing)

    End Sub


    <TestMethod()>
    Public Sub WorldPopulationEngine_RunPopulationCampaign()
        Dim testEngine As New WorldPopulationEngine()

        Dim mockCampaign As IPopulationCampaign = Substitute.For(Of IPopulationCampaign)()

        testEngine.RunPopulationCampaign(mockCampaign)

        mockCampaign.ReceivedWithAnyArgs().Run(Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub WorldPopulationEngine_RunPopulationCampaign_NullCampaign()
        Dim testEngine As New WorldPopulationEngine()

        testEngine.RunPopulationCampaign(Nothing)

    End Sub

    Private Sub GeneratePopulation(ByRef population As List(Of Person))
        Dim newPerson As New Person(0)
        population.Add(newPerson)
        population.Add(newPerson)
        population.Add(newPerson)
        population.Add(newPerson)
        population.Add(newPerson)

        newPerson = New Person(1)

        population.Add(newPerson)
        population.Add(newPerson)
        population.Add(newPerson)

        newPerson = New Person(2)
        population.Add(newPerson)

    End Sub

End Class