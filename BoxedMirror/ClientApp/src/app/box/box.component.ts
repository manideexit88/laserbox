
import { Component } from '@angular/core';
import { BoxService } from '../services/box.service';

@Component({
  selector: 'app-box-component',
  templateUrl: './box.component.html'
})
export class BoxComponent {
    loading: boolean = false; 
    file: File = null; 
    boxInput: BoxInput;
    laserExit: Room;
    showResult: boolean = false;

    // Inject service 
    constructor(private boxService: BoxService) {
    }

    // On file Select
    onChange(event) {
        this.file = event.target.files[0];
    }

    // OnClick of button Upload
    processInput() {
        this.loading = !this.loading;
        let fileReader = new FileReader();
        fileReader.onload = (e) => {
            this.parseAndReadBoxInput(fileReader.result);
            this.boxService.processLaser(this.boxInput).subscribe(result => {
                this.laserExit = result;
                this.loading = !this.loading;
                this.showResult = true;
            }, error => console.error(error));
        }
        fileReader.readAsText(this.file);
    }

    // Parse input file
    parseAndReadBoxInput(fileInput: string | ArrayBuffer) {
        var lines = fileInput.toString().split("\r\n");
        var breakCounter: number = 0;
        var columns: number = 0;
        var rows: number = 0;
        var laserEntry: Room;
        var mirrors: Mirror[] = [];
  
        for (var i = 0; i < lines.length; i++) {
            var line = lines[i];
            if (line.trim() == "-1") {
                breakCounter++;
                continue;
            } else {
                switch (breakCounter) {
                    case 0: // Read Dimentions
                        var dimentions = line.trim().split(",");
                        rows = parseInt(dimentions[1]);
                        columns = parseInt(dimentions[0]);
                        break;
                    case 1: // Read Mirror
                        var mirrorLine = line.trim().split(",");
                        var mirrorMeta = mirrorLine[1].replace(/[^a-zA-Z]+/g, '');
                        var mirror: Mirror = {
                              row: parseInt(mirrorLine[1])
                            , column: parseInt(mirrorLine[0])
                            , leaning: mirrorMeta[0]
                            , reflection: mirrorMeta.length > 1 ? mirrorMeta[1] : "RL"
                        };
                        mirrors.push(mirror);
                        break;
                    case 2: // Read LaserEntry
                        var door = line.trim().split(",");
                        laserEntry = {
                            row: parseInt(door[1])
                            , column: parseInt(door[0]) 
                            , orientation: door[1][door[1].length - 1]
                        }
                        break;
                    case 3:
                        return;
                }
            }
        }

        this.boxInput = {
              rows: rows
            , columns: columns
            , mirrors: mirrors
            , entry: laserEntry
        }
    }
}

