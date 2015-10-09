Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports System.Collections.Generic
Imports PersonPopulationCampaigns

<TestClass()> Public Class WorldPopulationDecreaseUnitTests

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedZero()

        Dim testingCampaign As New WorldPopluationDecreaseCampaign(New Random(0), 30)

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(15, popluation.Count)

    End Sub

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedOneHundred()

        Dim testingCampaign As New WorldPopluationDecreaseCampaign(New Random(100), 30)

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(16, popluation.Count)

    End Sub

    <TestMethod()>
<ExpectedException(GetType(ArgumentNullException))>
    Public Sub worldPopulationDecrease_NullRandom()
        Dim testingCampaign As New WorldPopluationDecreaseCampaign(Nothing, 30)
    End Sub

    <TestMethod()>
  <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationDecrease_NegChance()
        Dim testingCampaign As New WorldPopluationDecreaseCampaign(New Random(0), -30)
    End Sub

    <TestMethod()>
  <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationDecrease_TooHighChance()
        Dim testingCampaign As New WorldPopluationDecreaseCampaign(New Random(0), 130)
    End Sub

    Private Function GenerateStandardPopulation() As List(Of IResourceGenerator)
        Dim returnValue As New List(Of IResourceGenerator)

        'poor people
        Dim newPerson As New Person(0)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)

        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)

        newPerson = New Person(1)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)

        newPerson = New Person(2)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)
        returnValue.Add(newPerson)

        Return returnValue
    End Function

End Class