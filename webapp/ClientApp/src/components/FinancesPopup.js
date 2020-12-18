import IconButton from "@material-ui/core/IconButton";
import AttachMoneyOutlinedIcon from '@material-ui/icons/AttachMoneyOutlined';
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import React from "react";
import TableRow from "@material-ui/core/TableRow";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import Table from "@material-ui/core/Table";
import Paper from "@material-ui/core/Paper";

export const FinancesPopup = ({setOpenFinances, openFinances, orgNr, setFinances, finances}) => (
    <div>
        <IconButton
            aria-label="expand row"
            size="small"
            variant="outlined"
            color="primary"
            onClick={e => {
                e.stopPropagation();
                setOpenFinances(true);
                fetchData(orgNr, setFinances);
            }}
        >
            <AttachMoneyOutlinedIcon />
        </IconButton>
        <Dialog
            open={openFinances}
            onClose={() => setOpenFinances(false)}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
            maxWidth="sm"
            fullWidth
        >
            <DialogTitle >
                Finansinformasjon
            </DialogTitle>
            <DialogContent dividers>
                <div style={{whiteSpace: 'pre-line'}}>
                {finances ? renderFinances(finances) : <p>Finansinformasjon ikke funnet</p>}
                </div>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpenFinances(false)} color="primary">
                    Lukk
                </Button>
            </DialogActions>
        </Dialog>
    </div>
);

async function fetchData(orgNr, setFinances) {
        const response = await fetch(`queryFinances/${orgNr}`);
    
    if (response.status === 200) {
        const resJson = await response.json();
        await setFinances(resJson);
    }
}

function renderFinances(finances) {
    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Sum egenkapital</TableCell>
                        <TableCell>Kortsiktig gjeld</TableCell>
                        <TableCell>Langsiktig gjeld</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                        <TableRow>
                            <TableCell>{finances["egenkapital"]["sumEgenkapital"]} NOK</TableCell>
                            <TableCell>{finances["gjeldOversikt"]["kortsiktigGjeld"]["sumKortsiktigGjeld"]} NOK</TableCell>
                            <TableCell>{finances["gjeldOversikt"]["langsiktigGjeld"]["sumLangsiktigGjeld"]} NOK</TableCell>
                        </TableRow>
                </TableBody>
            </Table>
        </TableContainer>
    );
}