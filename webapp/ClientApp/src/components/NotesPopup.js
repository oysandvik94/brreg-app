import IconButton from "@material-ui/core/IconButton";
import {CommentOutlined} from "@material-ui/icons";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import React from "react";

export const NotesPopup = ({ setOpenNotes, openNotes, valueNotes}) => (
    <div>
        <IconButton
            aria-label="expand row"
            size="small"
            variant="outlined"
            color="primary"
            onClick={e => {
                e.stopPropagation();
                setOpenNotes(true);
            }}
            hidden={!valueNotes}
        >
            <CommentOutlined />
        </IconButton>
        <Dialog
            open={openNotes}
            onClose={() => setOpenNotes(false)}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
            maxWidth="sm"
            fullWidth
        >
            <DialogTitle >
                Notat
            </DialogTitle>
            <DialogContent dividers>
                <div style={{whiteSpace: 'pre-line'}}>
                    {valueNotes}
                </div>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpenNotes(false)} color="primary">
                    Lukk
                </Button>
            </DialogActions>
        </Dialog>
    </div>
);