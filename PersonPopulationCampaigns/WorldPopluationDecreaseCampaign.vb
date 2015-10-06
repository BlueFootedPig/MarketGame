Imports Core

Public Class WorldPopluationIncreaseCampaign
    Implements IPopulationCampaign
    Private odds As Double
    Private random As Random
    Public Sub New(randomGenerator As Random, oddsOfBirth As Double)
        If randomGenerator Is Nothing Then Throw New ArgumentNullException("randomGenerator", "randomGenerator cannot be null.")
        If oddsOfBirth < 0 OrElse oddsOfBirth > 100 Then Throw New ArgumentException("odds", "odds must be a percent between 0 and 100.")
        odds = oddsOfBirth
        random = randomGenerator
    End Sub

    Public Sub Run(ByRef population As IList(Of IResourceGenerator)) Implements IPopulationCampaign.Run

        Dim populationIncreases As New Dictionary(Of Integer, Integer)


        For Each subject As Person In population.OfType(Of Person)()
            Dim hadChild As Boolean = random.Next(0, 100) < (odds - subject.Income * 5)
            If hadChild Then
                If populationIncreases.ContainsKey(subject.Income) Then
                    populationIncreases(subject.Income) += 1
                Else
                    populationIncreases(subject.Income) = 1
                End If
            End If
        Next


        For Each incomeLevel As Integer In populationIncreases.Keys
            For counter As Integer = 0 To populationIncreases(incomeLevel)
                population.Add(New Person(incomeLevel))
            Next
        Next


    End Sub
End Class