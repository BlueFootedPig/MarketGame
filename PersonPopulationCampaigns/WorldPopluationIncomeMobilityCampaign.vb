Imports Core

Public Class WorldPopluationMobilityCampaign
    Implements IPopulationCampaign

    Private random As Random
    Private odds As Double
    Public Sub New(randomGenerator As Random, oddsOfDeath As Double)
        If randomGenerator Is Nothing Then Throw New ArgumentNullException("randomGenerator", "randomGenerator cannot be null.")
        If oddsOfDeath < 0 OrElse oddsOfDeath > 100 Then Throw New ArgumentException("odds", "odds must be a percent between 0 and 100.")

        odds = oddsOfDeath
        random = randomGenerator
    End Sub

    Public Sub Run(ByRef population As IList(Of IResourceGenerator)) Implements IPopulationCampaign.Run

        Dim populationDecreases As New List(Of Person)


        For Each subject As Person In population
            Dim hasDied As Boolean = random.Next(0, 100) < (odds - subject.Income * 5)
            If hasDied Then
                populationDecreases.Add(subject)
            End If
        Next


        For Each subject As Person In populationDecreases
            population.Remove(subject)
        Next


    End Sub
End Class