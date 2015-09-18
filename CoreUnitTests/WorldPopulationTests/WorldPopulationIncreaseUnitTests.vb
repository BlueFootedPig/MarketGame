Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()> Public Class WorldPopulationIncreaseUnitTests

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedZero()

        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(0))

        Dim popluation As List(Of Person) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(23, popluation.Count)

    End Sub

    <TestMethod()>
    Public Sub WorldPopulationIncrease_SeedOneHundred()

        Dim testingCampaign As New WorldPopluationIncreaseCampaign(New Random(100))

        Dim popluation As List(Of Person) = GenerateStandardPopulation()

        testingCampaign.Run(popluation)

        Assert.AreEqual(21, popluation.Count)

    End Sub

    Private Function GenerateStandardPopulation() As List(Of Person)
        Dim returnValue As New List(Of Person)

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