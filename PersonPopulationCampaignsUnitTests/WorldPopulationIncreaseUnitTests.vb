Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports PersonPopulationCampaigns
Imports System.Collections.Generic

<TestClass()> Public Class WorldPopulationIncreaseUnitTests

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedZero()

        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(0), 30)

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(23, popluation.Count)

    End Sub

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedOneHundred()

        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(100), 30)

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(21, popluation.Count)

    End Sub

    <TestMethod()>
  <ExpectedException(GetType(ArgumentNullException))>
    Public Sub worldPopulationIncrease_NullRandom()
        Dim testingCampaign As New WorldPopluationIncreaseCampaign(Nothing, 30)
    End Sub

    <TestMethod()>
  <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationIncrease_NegChance()
        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(0), -30)
    End Sub

    <TestMethod()>
  <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationIncrease_TooHighChance()
        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(0), 130)
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