Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports System.Collections.Generic
Imports PersonPopulationCampaigns
Imports System.Linq

<TestClass()> Public Class WorldPopulationGainTagUnitTests

    <TestMethod()>
    Public Sub worldPopulationGainTag_SeedZero()

        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(0), 30, "TestTag")

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(4, popluation.Where(Function(n) n.Tags.Contains("TestTag")).Count)

    End Sub

    <TestMethod()>
    Public Sub worldPopulationGainTag_SeedOneHundred()

        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(100), 30, "TestTag")

        Dim popluation As List(Of IResourceGenerator) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(2, popluation.Where(Function(n) n.Tags.Contains("TestTag")).Count)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationGainTag_NegOdds()
        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(0), -10, "TestTag")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationGainTag_TooHighOfOdds()
        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(0), 110, "TestTag")
    End Sub
    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationGainTag_BlankTag()
        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(0), 50, "")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub worldPopulationGainTag_NullTag()
        Dim testingCampaign As New GainTagWorldPopluationCampaign(New Random(0), 50, Nothing)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub worldPopulationGainTag_NullRandom()
        Dim testingCampaign As New GainTagWorldPopluationCampaign(Nothing, 50, "Test")
    End Sub
    'Adds 18 people
    Private Function GenerateStandardPopulation() As List(Of IResourceGenerator)
        Dim returnValue As New List(Of IResourceGenerator)

        '10 poor people
        Dim newPerson As New Person(0)
        returnValue.Add(newPerson)
        newPerson = New Person(0)
        returnValue.Add(newPerson)
        newPerson = New Person(0)
        returnValue.Add(newPerson)
        newPerson = New Person(0)
        returnValue.Add(newPerson)
        newPerson = New Person(0)
        returnValue.Add(newPerson)

        newPerson = New Person(0)
        returnValue.Add(newPerson)

        newPerson = New Person(0)
        returnValue.Add(newPerson)

        newPerson = New Person(0)
        returnValue.Add(newPerson)

        newPerson = New Person(0)
        returnValue.Add(newPerson)

        newPerson = New Person(0)
        returnValue.Add(newPerson)

        '5 middle class
        newPerson = New Person(1)
        returnValue.Add(newPerson)
        newPerson = New Person(1)
        returnValue.Add(newPerson)
        newPerson = New Person(1)
        returnValue.Add(newPerson)
        newPerson = New Person(1)
        returnValue.Add(newPerson)
        newPerson = New Person(1)
        returnValue.Add(newPerson)

        '3 rich
        newPerson = New Person(2)
        returnValue.Add(newPerson)
        newPerson = New Person(2)
        returnValue.Add(newPerson)
        newPerson = New Person(2)
        returnValue.Add(newPerson)

        Return returnValue
    End Function

End Class