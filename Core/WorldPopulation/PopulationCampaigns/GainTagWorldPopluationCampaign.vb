
Public Class GainTagWorldPopluationCampaign
    Implements IPopulationCampaign

    Private random As Random
    Private odds As Double
    Private tag As String

    ''' <summary>
    ''' Runs the campaign.
    ''' </summary>
    ''' <param name="randomGenerator">Object to provide random.</param>
    ''' <param name="oddsOfGettingTag">Percent chance of gaining tag</param>
    ''' <remarks></remarks>
    Public Sub New(randomGenerator As Random, oddsOfGettingTag As Double, tagToAdd As String)
        If randomGenerator Is Nothing Then Throw New ArgumentNullException("randomGenerator", "Must have a valid random generator.")
        If oddsOfGettingTag <= 0 OrElse oddsOfGettingTag >= 100 Then Throw New ArgumentException("oddsOfGettingTag", "Odds must be between 0 and 100.")
        If String.IsNullOrEmpty(tagToAdd) Then Throw New ArgumentException("tagToAdd", "tagToAdd cannot be null or empty.")
        random = randomGenerator
        odds = oddsOfGettingTag
        tag = tagToAdd
    End Sub

    Public Sub Run(ByRef population As IList(Of IResourceGenerator)) Implements IPopulationCampaign.Run

        Dim populationIncreases As New Dictionary(Of Integer, Integer)


        For Each subject As Person In population
            If random.Next(0, 100) < odds Then subject.Tags.Add(tag)
        Next


    End Sub
End Class