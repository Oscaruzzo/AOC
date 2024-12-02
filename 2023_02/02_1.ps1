function ParseCubeSet([string] $CubeSet) {
    $set = @{}
    foreach ($cubes in $CubeSet.Split(',').Trim()) {
        $n, $c = $cubes.Split(' ')
        $set[$c] = [int] $n
    }
    return $set
}

function IsGoodSet([hashtable] $Set, [hashtable] $Reference) {
    foreach ($color in $Set.Keys) {
        if (($null -eq $Set[$color]) -or ($Reference[$color] -ge $Set[$color])) {
            # do nothing
        } else {
            return $false
        }
    }
    return $true
}

function ParseGame([string] $Line) {
    $splitLine = $Line.Split(':')
    $header = $splitLine[0]
    $gameIndex = [int32] ($header | Select-String '[0-9]+').Matches.Value
    $gameData = $splitLine[1]

    $sets = @()
    foreach ($cubeSet in $gameData.Split(';').Trim()) {
        $sets += ParseCubeSet($cubeSet)
    }

    return $gameIndex, $sets
}

function EvaluateGame([string] $Line, [hashtable] $Reference) {
    $gameIndex, $sets = ParseGame($Line)
    foreach ($set in $sets) {
        if (IsGoodSet -Set $set -Reference $Reference) {
            # do nothing
        } else {
            return 0
        }
    }
    return $gameIndex
}

$line = 'Game 17: 6 red, 13 blue, 8 green; 12 blue, 7 green, 9 red; 19 blue, 5 red; 2 green, 8 red, 14 blue'
$reference = ParseCubeSet('12 red, 13 green, 14 blue')

$n = 0
foreach ($line in Get-Content .\input2.txt) {
    $n += EvaluateGame -Line $line -Reference $reference
}

$n